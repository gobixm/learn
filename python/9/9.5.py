class Gen:
    static = 0

    def __init__(self):
        self.counter = 0

    def next(self):
        self.counter = self.counter + 1
        return self.counter


class ConcreteGen(Gen):
    pass

gen = Gen()

print(gen.next())
print(gen.next())
print(gen.static)

concrete = ConcreteGen()
print(concrete.next())
print(concrete.next())
