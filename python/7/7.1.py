for x in range(1, 11):
    print(repr(x**2).rjust(10), repr(x**4).rjust(10), repr(x**8).rjust(10))


print('{} {} {!r}'.format(1, 2, '3'))
print('{}'
      ' {}'
      ' {!r}'
      .format(1, 2, '3'))
