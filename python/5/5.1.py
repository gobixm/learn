l = []
l.append(1)
print(l)

l.extend([2, 3])
print(l)

l.insert(0, 0)
print(l)

print(l.pop())
print(l)

print(l.index(2))

l.extend([0] * 3)
print(l)
print(l.count(0))

l.sort()
print(l)

l.reverse()
print(l)

squares = list(map(lambda x: x**2, l))
print(squares)

squares = [x**2 for x in range(10) if x > 5]
print(squares)
del squares[0]
print(squares)

while(len(l) > 0):
    print(l.pop())
