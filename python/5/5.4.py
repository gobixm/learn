basket = {1, 1, 1, 1}

print(basket)

basket.add(2)
basket.add(3)
basket.add(4)
basket.add(4)

print(basket)

set1 = set(range(5))
set2 = set(range(3, 8))

print(set1, set2)

print(set1 - set2)
print(set2 - set1)
print(set1 | set2)
print(set1 & set2)
print(set1 ^ set2)
