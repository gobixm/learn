class Iterable:

    def __init__(self, array):
        self.array = array
        self.iter = array.__iter__()

    def __iter__(self):
        return self.iter

    def __next__(self):
        return self.iter.__next__()

iterable = Iterable([1, 2, 3])

for i in iterable:
    print(i)
