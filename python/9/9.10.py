def generator(data):
    for i in range(0, 5):
        yield data[i]

arr = [1, 2, 3, 4, 5, 6, 7, 8]

for i in generator(arr):
    print(i)
