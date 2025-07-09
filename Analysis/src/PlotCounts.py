import pandas as pd
import matplotlib.pyplot as plt
import numpy as np

fontsizer = 20

# Load the two CSV files
table1 = pd.read_csv("../data/raw_counts.csv", sep=';')
table2 = pd.read_csv("../data/weighted_counts.csv", sep=';')

# Extract group labels
groups = table1['Group']
table1_data = table1.drop(columns='Group')
table2_data = table2.drop(columns='Group')

# Set up the bar positions
x = np.arange(len(groups))
width = 0.35

fig, ax = plt.subplots(figsize=(14, 12), constrained_layout=True)
ax.set_xlim(x[0] - width, x[-1] + width)

# Define consistent colors for each column
colors = plt.cm.tab20.colors
column_colors = {col: colors[i % len(colors)] for i, col in enumerate(table1_data.columns)}

# Plot stacked bars for table1 (Raw)
bottom1 = np.zeros(len(groups))
for i, col in enumerate(table1_data.columns):
    bars = ax.bar(x - width/2, table1_data[col], width, label=col, bottom=bottom1, color=column_colors[col])
    if i == 0:
        raw_bars = bars  # Save the first layer of bars to get positions
    bottom1 += table1_data[col].values

# Plot stacked bars for table2 (Weighted)
bottom2 = np.zeros(len(groups))
for i, col in enumerate(table2_data.columns):
    bars = ax.bar(x + width/2, table2_data[col], width, bottom=bottom2, color=column_colors[col])
    if i == 0:
        weighted_bars = bars  # Save the first layer of bars to get positions
    bottom2 += table2_data[col].values

# Formatting
ax.set_ylabel('Occurrences', fontsize=fontsizer)
ax.set_xlabel('Group', fontsize=fontsizer, labelpad=85)
ax.set_xticks(x)
ax.set_xticklabels(groups, rotation=45, fontsize=fontsizer)
ax.tick_params(axis='y', labelsize=fontsizer)
ax.yaxis.grid(True)

# Create custom legend with one entry per column
handles = [plt.Rectangle((0,0),1,1, color=column_colors[col]) for col in table1_data.columns]
labels = table1_data.columns

# Create legend handles
handles = [plt.Rectangle((0, 0), 1, 1, color=column_colors[col]) for col in table1_data.columns]
labels = table1_data.columns

# Place legend above the plot, outside the axes
fig.legend(
    handles, labels,
    title='Codes',
    loc='outside upper center',
    ncol=3,
    fontsize=fontsizer,
    title_fontsize=fontsizer,
    frameon=False
)

# Adjust the subplot to leave space for the legend
# plt.subplots_adjust(top=0.82)

# Capture actual bar positions for accurate label placement
raw_bar_centers = [bar.get_x() + bar.get_width() / 2 for bar in raw_bars]
weighted_bar_centers = [bar.get_x() + bar.get_width() / 2 for bar in weighted_bars]

# Determine y offset for labels
y_offset = -max(bottom1.max(), bottom2.max()) * 0.09

# Add group labels below x-axis using actual bar positions
for raw_x, weighted_x in zip(raw_bar_centers, weighted_bar_centers):
    ax.text(raw_x, y_offset, 'Raw', ha='right', va='top', fontsize=fontsizer, rotation=60)
    ax.text(weighted_x, y_offset, 'Weighted', ha='right', va='top', fontsize=fontsizer, rotation=60)

# plt.tight_layout()
plt.show()

