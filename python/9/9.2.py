class TheClass:
    var = 1

    def __init__(self):
        self.data = []

    def __str__(self):
        return 'str'

    def __repr__(self):
        return 'repr'

    def foo(self):
        return 'call foo'


x = TheClass()

print(repr(x))
print(str(x))

print(x.foo())

y = x.foo

print(y())
