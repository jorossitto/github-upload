# -*- coding: utf-8 -*-
"""
Created on Mon Jul  8 15:00:19 2019

@author: Rossitj
"""

import numpy as np
import matplotlib.pyplot as plt

N = 1000
D = 2
NN = int(N/2)

innerRadius = 5
outerRadius = 10


def Donut(randomRange, radius):
    R1 = np.random.randn(randomRange) + radius
    theta = 2*np.pi*np.random.random(randomRange)
    result = np.concatenate([[R1 * np.cos(theta)], [R1 * np.sin(theta)]]).T
    return result

xInner = Donut(NN, innerRadius)
xOuter = Donut(NN, outerRadius)


x= np.concatenate([xInner, xOuter])
t = np.array([0]*(NN) + [1] * (NN))

plt.scatter(x[:,0], x[:,1], c=t)
plt.show()

ones = np.array([[1]* N]).T
r = np.zeros((n,1))
for i in xrange(N):
    r[i] = np.sqrt(x[i,:].dot(x[i,:]))

xb = np.concatenate((ones, r, x), axis = 1)
w = np.random.rand(D+2)

z = Xb.dot(w)

def sigmoid(z):
    return 1/(1+np.exp(-z))

y = sigmoid(z)

#cross entropy error
def cross_entropy(t,y):
    e = 0
    for i in xrange(N):
        if t[i] == 1:
            e -= np.log(y[i])
        else:
            e-= np.log(1-y[i])
    return e

learningRate = 0.0001
error = []
for i in xrange(5000):
    e = cross_entropy(t,y)