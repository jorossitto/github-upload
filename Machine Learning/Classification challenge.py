# -*- coding: utf-8 -*-
"""
Created on Thu Oct 31 14:30:02 2019

@author: Rossitj
"""

from sklearn.neighbors import KNeighborsClassifier
knn = KNeighborsClassifier(n_neighbors=6)
knn.fit(iris['data'], iris['target'])

iris['data'].shape
iris['target'].shape

prediction = knn.predict(X_new)
x_new.shape


print('Prediction {})
