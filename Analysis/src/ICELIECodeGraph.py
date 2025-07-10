import pandas as pd
import networkx as nx
import matplotlib.pyplot as plt
import textwrap
import seaborn as sns


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

    # Define custom node sizes based on weighting
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
    # print(clusters)

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
    plt.show()


if __name__ == "__main__":
    matrix_file = "../data/CodeVCode.csv"  # Path to the co-occurrence matrix CSV file
    fontsize = 20  # Set the default font size for the plot
    cooccurrence_cluster_graph(matrix_file, fontsizer=fontsize)

