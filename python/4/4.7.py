def foo(a, b=-1, c=-2):
    print(a + b + c)


foo(3)
foo(2, 0)
foo(2, 0, 0)

i = 99


def default(a=i):
    print(a)

i = 10
default()


def closure():
    print(i)

i = 20
closure()


def named(named1, named2=None, named3=None):
    print(named1)
    print(named2)
    print(named3)

named(0)
named(0, named3='named3')


def varargs(*args, **keys):
    for arg in args:
        print(arg)
    for key in keys:
        print(key, keys[key])

varargs('param1',
        'param2',
        key1='value1',
        key2='value2')


def parrot(first, second, third=3):
    print(first, second, third)

params = {'first': 1, 'second': 2}

parrot(**params)


def incrementor(n):
    return lambda x: x + n

f = incrementor(10)
print(f(0))
print(f(1))


def documentation() -> str:
    """Documentation

    here
    """
    return ""

print(documentation.__doc__)
print(documentation.__annotations__)
