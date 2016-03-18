array = [1, 2, 3, 5, 8, 13]
print(array)
print(array[1:-1])
print(array[10:-10])
print(array + array)
array[0] = 0
print(array)
array[:-1] = [1, 1, 1, 1, 1, 1, 1, 1]
print(array)
array[-1:] = []
print(array)
print(len(array))
i = 0
while i < len(array):
    array[i] = i
    i = i + 1
    print(array)
