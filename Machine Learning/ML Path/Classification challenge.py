# -*- coding: utf-8 -*-
"""
Created on Thu Oct 31 14:30:02 2019

@author: Rossitj
"""


# Import KNeighborsClassifier from sklearn.neighbors
from sklearn.neighbors import KNeighborsClassifier
import pandas as pd
from pandas import DataFrame

Data = pd.read_csv(r'house-votes-84.csv', names = ['party', 'q1', 'q2'])
df = pd.DataFrame(Data)
print(df.head())


# Create arrays for the features and the response variable
y = df['party'].values
X = df.drop('party', axis=1).values

# Create a k-NN classifier with 6 neighbors
knn = KNeighborsClassifier(n_neighbors = 6)

# Fit the classifier to the data
knn.fit(X,y)

# Import KNeighborsClassifier from sklearn.neighbors
from sklearn.neighbors import KNeighborsClassifier 

# Create arrays for the features and the response variable
y = df['party'].values
X = df.drop('party', axis=1).values

# Create a k-NN classifier with 6 neighbors: knn
knn = KNeighborsClassifier(n_neighbors = 6)

# Fit the classifier to the data
knn.fit(X,y)

# Predict the labels for the training data X
y_pred = knn.predict(X)

# Predict and print the label for the new data point X_new
new_prediction = knn.predict(X_new)
print("Prediction: {}".format(new_prediction))

from sklearn.model_selection import train_test_split
X_train, X_test, y_train, y_test = train_test_split(X,y,test_size=0.3, random_state = 21, stratify=y)

knn = KNeighborsClassifier(n_neighbors = 8)

knn.fit(X_train, y_train)

y_pred = knn.predict(X_test)



