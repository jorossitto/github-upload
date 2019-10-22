# -*- coding: utf-8 -*-
"""
Spyder Editor

This is a temporary script file.
"""

rate = 1.04
term = 9
amount = 18.13

interest = rate ** term

print (interest)
total = amount * interest
print(total)


"""
def compoundInterest(years, rate, amount):
    return (years ** rate) * amount
"""

"""
Roth Vs Ira
"""
"""
amount = 12

iraAmount = amount
rothAmount = amount * .5
tenPercentPenalty = amount * .9
doubleTaxes = amount * .5

rate = 1.1
years = 40
iraAmount = compoundInterest(years, rate, iraAmount)
rothAmount = compoundInterest(years, rate, rothAmount)
tenPercentPenalty = compoundInterest(years, rate, tenPercentPenalty)
doubleTaxes = compoundInterest(years, rate, doubleTaxes)

iraAmount = iraAmount * .5
doubleTaxes = doubleTaxes *.5

print("Ira " + str(iraAmount))
print("Roth " + str(rothAmount))
print("Penalty " + str(tenPercentPenalty))
print("Double Taxes " + str(doubleTaxes))
"""

