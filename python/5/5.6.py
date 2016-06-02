dic = {}
for i, v in enumerate(['a', 'b', 'c']):
    dic[i] = v

print(dic)
print('')

indices = range(10)
values = list('abcde')

for i, v in zip(indices, values):
    print(i, v)
