array = [0, 1, 2, 3, 4, 5, 6]
for d in array:
    print(d)

for x in array:
    array[x] = x * x

print(array)

for x in array[:]:
    array.insert(x, x * x)

print(array)
