import sys

try:
    0 / 0
except:
    print(sys.exc_info()[0])


try:
    pass
except IOError:
    pass
else:
    print('noerror')


try:
    raise Exception('arg1', 'arg2')
except Exception as ex:
    print(ex.args)
    a1, a2 = ex.args
    print(a1, a2)


raise NameError('NE')
