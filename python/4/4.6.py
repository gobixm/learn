def foo(n):
    print(list(range(-n, n + 1)))
    return n

foo(3)
foo(5)

print(foo)
print(foo(3))


def fool(m, func):
    for i in range(m):
        func(i)

fool(5, foo)
