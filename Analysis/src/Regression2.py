import pandas as pd
import numpy as np
import seaborn as sns
import matplotlib.pyplot as plt
from sklearn.linear_model import LinearRegression
from sklearn.feature_selection import mutual_info_regression
from sklearn.preprocessing import StandardScaler

# Load dataset
df_full = pd.read_csv('../data/regression_input.csv', index_col=0, header=0, sep=';')  # Replace with your actual file path

# Define dependent and independent variables
dependent_var = 'Eval6'  # Replace with your actual target

# Suppose you want columns from index 2 to 6 (inclusive of 2, exclusive of 7)
independent_vars = df_full.columns[6:].tolist()
df = df_full[independent_vars]

# Keep only numeric data
df = df.select_dtypes(include=[np.number])

# Standardize features
scaler = StandardScaler()
X = pd.DataFrame(scaler.fit_transform(df[independent_vars]), columns=independent_vars)
y = df_full[dependent_var]

# Pearson correlation
correlations = X.corrwith(y)
correlations.sort_values(ascending=False).plot(kind='bar', title='Pearson Correlation with Target')
plt.ylabel('Correlation Coefficient')
plt.tight_layout()
plt.show()

# Mutual Information
mi = mutual_info_regression(X, y)
mi_series = pd.Series(mi, index=independent_vars)
mi_series.sort_values(ascending=False).plot(kind='bar', title='Mutual Information with Target')
plt.ylabel('MI Score')
plt.tight_layout()
plt.show()

# Linear Regression Coefficients
model = LinearRegression()
model.fit(X, y)
coefs = pd.Series(model.coef_, index=independent_vars)
coefs.sort_values(ascending=False).plot(kind='bar', title='Linear Regression Coefficients')
plt.ylabel('Coefficient Value')
plt.tight_layout()
plt.show()
