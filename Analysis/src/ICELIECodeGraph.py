import pandas as pd
import networkx as nx
import matplotlib.pyplot as plt
import textwrap


fontsizer = 20

# Load the CSV file
file_path = "../data/CodeVCode.csv"  # Change this to your actual file path
df = pd.read_csv(file_path, sep=';', index_col=0, header=0)

# Clean column names and index (remove prefixes like "1 :")
df.columns = [col.split(":")[-1].strip() for col in df.columns]
df.index = [idx.split(":")[-1].strip() for idx in df.index]

print(df.index)

# Create a graph
G = nx.Graph()

# Add edges with weights
for i, row_label in enumerate(df.index):
    for j, col_label in enumerate(df.columns):
        if row_label == col_label:
            continue  # Skip diagonal
        weight = df.iat[i, j]
        if pd.notna(weight) and weight > 0:
            G.add_edge(row_label, col_label, weight=weight)

# Invert weights for layout (stronger weight = shorter edge)
inv_weights = {(u, v): 1 / d['weight'] for u, v, d in G.edges(data=True)}


wrapped_labels = {
    node: '\n'.join(textwrap.wrap(str(node), width=15))
    for node in G.nodes()
}


# Apply inverted weights temporarily for layout
pos = nx.spring_layout(G, weight=None, iterations=10000, seed=15, k=None)

# Manually adjust positions using inverted weights
for (u, v), inv_w in inv_weights.items():
    G[u][v]['inv_weight'] = inv_w


# Recompute layout using inverted weights
pos = nx.spring_layout(G, weight='inv_weight', iterations=1000, seed=10)

# Define custom node sizes (example: based on sum of weights)
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
node_sizes = [node_sizes_map.get(node, 10) * 100 for node in G.nodes()]  # Default size = 10

# Draw the graph using a force-directed layout
# pos = nx.spring_layout(G, weight='inv_weight', seed=13, iterations=1000)  # Seed for reproducibility, increase iterations for better layout
plt.figure(figsize=(30, 30))
neg_nodes = {'Negative'} # Replace with your own set of nodes
pos_nodes = {'Positive'} # Replace with your own set of nodes

node_colors = ['red' if node in neg_nodes else 'green' if node in pos_nodes else 'skyblue' for node in G.nodes()]

# rotated_pos = {node: (-y, x) for node, (x, y) in pos.items()}


nx.draw(G, pos, with_labels=True, labels=wrapped_labels, node_size=node_sizes, node_color=node_colors, font_size=fontsizer, edge_color='gray')
labels = nx.get_edge_attributes(G, 'weight')
nx.draw_networkx_edge_labels(G, pos, edge_labels=labels, font_size=fontsizer)
plt.title("Force-Directed Graph from CSV Data")
# plt.tight_layout()
plt.show()
