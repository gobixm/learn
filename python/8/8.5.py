class MyError(Exception):

    def __init__(self, value):
        self.value = value

    def __str__(self):
        return repr(self.value)


try:
    x = 'someval'
    raise MyError('the value')
except MyError as e:
    print(e)
    print(e.value)
finally:
    print(x)
