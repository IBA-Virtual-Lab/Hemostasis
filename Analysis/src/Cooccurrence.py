import pandas as pd
import networkx as nx
import matplotlib.pyplot as plt
import textwrap
import seaborn as sns
import statsmodels.api as sm
import textwrap


def analyze_community_influence(df, communities, dependent_var, community_sizes, wrap_width=30, fontsize=15):
    """
    Analyzes the influence of Louvain communities on a dependent variable using R-squared and average coefficients.

    Parameters:
        df (pd.DataFrame): The dataframe containing independent and dependent variables.
        communities (list of sets): List of sets, each containing variable names in a community.
        dependent_var (str): The name of the dependent variable column.
        wrap_width (int): Number of characters before wrapping x-axis labels.

    Returns:
        pd.DataFrame: Summary of R-squared and average signed coefficients for each community.
    """
    summary_data = []

    for i, community in enumerate(communities):
        X = df[list(community)]
        X = sm.add_constant(X)
        y = df[dependent_var]

        model = sm.OLS(y, X).fit()
        avg_coef = model.params.drop('const').mean()
        r_squared = model.rsquared

        summary_data.append({
            'Community': community,
            'R_squared': r_squared,
            'Avg_Coefficient': avg_coef
        })

    summary_df = pd.DataFrame(summary_data)
    summary_df['Size'] = community_sizes
    summary_df = summary_df.sort_values(by='Avg_Coefficient', ascending=True).reset_index(drop=True)
    summary_df.to_csv('../data/cluster_summary.csv', index=False, sep=';')

    # Create wrapped labels for x-axis
    summary_df['Label'] = summary_df['Community'].apply(
        lambda s: '\n'.join(textwrap.wrap(', '.join(s), wrap_width))
    )

    # Plotting
    fig, ax1 = plt.subplots(figsize=(10, 10))

    ax1.bar(summary_df['Label'], summary_df['R_squared'], color='skyblue', label='R-squared', width=0.95)
    ax1.set_ylabel('R-squared', color='blue', fontsize=fontsize)
    ax1.tick_params(axis='y', labelcolor='blue', labelsize=fontsize)
    ax1.set_xticklabels(summary_df['Label'], rotation=90, ha='center', fontsize=fontsize)

    ax2 = ax1.twinx()
    ax2.plot(summary_df['Label'], summary_df['Avg_Coefficient'], color='red', linestyle='None', marker='o', label='Avg Coefficient')
    for i, val in enumerate(summary_df['Avg_Coefficient']):
        ax2.text(i, val, f'{val:.2f}', color='red', ha='center', fontsize=fontsize, va='bottom' if val >= 0 else 'top')
    ax2.set_ylabel('Average Coefficient', color='red', fontsize=fontsize)
    ax2.tick_params(axis='y', labelcolor='red', labelsize=fontsize)

    # plt.title('Community Influence on Dependent Variable')
    fig.tight_layout()
    plt.savefig('../data/metrics.png', format='png', dpi=600)  # Save the plot as a PNG file
    # plt.show()

    return summary_df


def cooccurrence_cluster_graph(file_path, fontsizer=20):
    """
    Function to create a co-occurrence graph from a CSV file containing a co-occurrence matrix.
    The graph is visualized using NetworkX and Matplotlib.
    """

    # Load co-occurrrence matrix from CSV file
    df = pd.read_csv(file_path, sep=';', index_col=0, header=0)

    # Clean column names and index (remove prefixes like "1 :")
    df.columns = [col.split(":")[-1].strip() for col in df.columns]
    df.index = [idx.split(":")[-1].strip() for idx in df.index]
    # print(df.index)

    # Create a graph
    G = nx.Graph()

    # Add edges using weights from co-occurrence matrix
    for i, row_label in enumerate(df.index):
        for j, col_label in enumerate(df.columns):
            if row_label == col_label:
                continue  # Skip diagonal
            weight = df.iat[i, j]
            if pd.notna(weight) and weight > 0:
                G.add_edge(row_label, col_label, weight=weight)

    # Wrap text for codes with names longer than a threshold width
    wrapped_labels = {
        node: '\n'.join(textwrap.wrap(str(node), width=15))
        for node in G.nodes()
    }

    # Compute layout using Fruchterman-Reingold force-directed algorithm. Increasing 'k' increases repulsion between nodes.
    pos = nx.spring_layout(G, weight='weight', iterations=1000, seed=10, k=0.5)

    # Define custom node sizes based on weighting occurrence counts by number of respondents represented
    node_sizes_map = {
        'Negative':123,
        'Positive':93,
        'Specific request':64,
        'UI problems':52,
        'Execution problems':25,
        'Over-automation':22,
        'Pedagogic problems':22,
        'Procedural Familiarization':16,
        'Prefer other sources':18,
        'Pipette interactivity':13,
        'Direction problems':13,
        'Insufficiently descriptive':14,
        'Physical Familliarization':10,
        'Repetition':10,
        'Request for Norsk':10,
        'Mismatch to real':10,
        'More emphasis on machine':11,
        'Visibility':7,
        'NA to respondant':4,
        'Simulation not needed':7,
        'Program not resetting':4
    }
    node_sizes = [node_sizes_map.get(node, 10) * 40 for node in G.nodes()]  # Default size = 10

    # Detect communities (clusters) using the Louvain Community Detection Algorithm
    clusters = nx.community.louvain_communities(G, resolution=2, seed=12)
    num_partitions = len(clusters)
    print(clusters)

    community_sizes = []
    for community in clusters:
        total_size = sum(node_sizes_map.get(node, 0) for node in community)
        community_sizes.append(total_size)
        print(community, 'Occurrences: ', total_size)

    df_full = pd.read_csv('../data/regression_input.csv', index_col=0, header=0,
                          sep=';')  # Replace with your actual file path
    df_vars = df_full.iloc[:, 5:]

    analyze_community_influence(df_vars, clusters, 'Eval6', community_sizes)

    # Create a color pallet for the number of clusters
    seaborn_palette_name = 'pastel'
    try:
        # seaborn.color_palette returns list of RGB tuples; create a ListedColormap of these colors for consistent use
        colors_list = sns.color_palette(seaborn_palette_name, n_colors=num_partitions)
        if num_partitions > 0:
             cmap = plt.matplotlib.colors.ListedColormap(colors_list)
        else: # Handle case with 0 partitions gracefully
            cmap = plt.matplotlib.colors.ListedColormap(['#CCCCCC']) # A neutral grey
    except ValueError:
        print(f"Specified palette '{seaborn_palette_name}' not found or invalid. Falling back to matplotlib.")
        if num_partitions <= 10:
            cmap = plt.cm.get_cmap('tab10', num_partitions)
        else:
            cmap = plt.cm.get_cmap('tab20', num_partitions)

    # Create a mapping from node to its partition index (color index)
    node_to_color_index = {}
    for i, cluster_set in enumerate(clusters):
        for node in cluster_set:
            if node in G: # Ensure the node exists in the graph
                node_to_color_index[node] = i

    # Create the list of colors for each node, in the same order as G.nodes()
    node_colors = [cmap(node_to_color_index.get(node, -1)) for node in G.nodes()]
    # .get(node, -1) handles cases where a node might not be in any partition,
    # assigning it the first color from the colormap (index -1 wraps around to last, but if no partitions
    # it defaults to 0). For robustness, ensure all graph nodes are covered by partitions.

    # Generate plot with labels.
    plt.figure(figsize=(15, 15))  # Create a matplotlib figure for plotting
    nx.draw(G, pos, with_labels=True, labels=wrapped_labels, node_size=node_sizes, node_color=node_colors,
            font_size=fontsizer, edge_color='gray')
    labels = nx.get_edge_attributes(G, 'weight')
    nx.draw_networkx_edge_labels(G, pos, edge_labels=labels, font_size=(fontsizer-5))
    plt.savefig('../data/cooccurrence.png', format='png', dpi=600)  # Save the plot as a PNG file
    # plt.show()

if __name__ == "__main__":
    matrix_file = "../data/CodeVCode.csv"  # Path to the co-occurrence matrix CSV file
    fontsize = 20  # Set the default font size for the plot
    cooccurrence_cluster_graph(matrix_file, fontsizer=fontsize)

