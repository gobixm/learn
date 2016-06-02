dick = {'x': 0, 'y': 1, 'z': 2}

print(list(dick.keys()))
print(sorted(list(dick.keys())))

del dick['y']

print(dick)


for k, v in dick.items():
    print(k, v)
