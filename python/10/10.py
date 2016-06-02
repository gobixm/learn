import os
print(os.getcwd())
print(dir(os))
print(help(os))

import glob

print(glob.glob('../*'))

import sys
print(sys.argv)

sys.stderr.write('std::error\n')

import random
print(random.choice(['apple', 'pearl', 'beer']))

print(random.sample(range(100), 10))
print(random.random())
print(random.randrange(10**1000))

# inet


def request():
    from urllib.request import urlopen
    with urlopen('http://localhost:3686/index.html#/') as response:
        for line in response:
            line.decode('utf-8-sig')
        print('responded')
request()

# detetime
from datetime import date
now = date.today()
print(now)
print(now - date(2016, 3, 24))

# timing
import timeit

print(timeit.timeit('request()', globals=globals(), number=1))
