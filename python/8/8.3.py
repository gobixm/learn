import sys

try:
    0 / 0
except:
    print(sys.exc_info()[0])
