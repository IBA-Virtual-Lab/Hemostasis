import pandas as pd
import numpy as np
import seaborn as sns
import matplotlib.pyplot as plt
from sklearn.feature_selection import mutual_info_regression
from itertools import combinations
import networkx as nx

# Load your dataset
df = pd.read_csv('../data/regression_input.csv', index_col=0, header=0, sep=';')  # Replace with your actual file path

# Pearson and Spearman correlations
pearson_corr = df.corr(method='pearson')
spearman_corr = df.corr(method='spearman')

# Mutual Information
def compute_mutual_info(df):
    mi_matrix = pd.DataFrame(index=df.columns, columns=df.columns)
    for col1, col2 in combinations(df.columns, 2):
        mi = mutual_info_regression(df[[col1]], df[col2])[0]
        mi_matrix.loc[col1, col2] = mi
        mi_matrix.loc[col2, col1] = mi
    np.fill_diagonal(mi_matrix.values, 1.0)
    return mi_matrix.astype(float)

mutual_info_corr = compute_mutual_info(df)

# Heatmaps
plt.figure(figsize=(15, 4))
for i, (title, matrix) in enumerate(zip(
    ['Pearson', 'Spearman', 'Mutual Information'],
    [pearson_corr, spearman_corr, mutual_info_corr]
)):
    plt.subplot(1, 3, i+1)
    sns.heatmap(matrix, annot=False, cmap='coolwarm', square=True)
    plt.title(f'{title} Correlation')
plt.tight_layout()
plt.show()

# Pair plot
sns.pairplot(df)
plt.show()

# Optional: Network graph for strong correlations
def plot_correlation_network(corr_matrix, threshold=0.7):
    G = nx.Graph()
    for col in corr_matrix.columns:
        G.add_node(col)
    for i, j in combinations(corr_matrix.columns, 2):
        weight = corr_matrix.loc[i, j]
        if abs(weight) >= threshold:
            G.add_edge(i, j, weight=weight)
    pos = nx.spring_layout(G)
    edges = G.edges(data=True)
    weights = [abs(d['weight']) for (u, v, d) in edges]
    nx.draw(G, pos, with_labels=True, node_color='lightblue',
            edge_color=weights, width=2.0, edge_cmap=plt.cm.coolwarm)
    plt.title(f'Correlation Network (|corr| ≥ {threshold})')
    plt.show()

# Example: Pearson correlation network
plot_correlation_network(pearson_corr, threshold=0.7)
