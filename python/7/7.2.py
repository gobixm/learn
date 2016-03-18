import json

with open('file', 'w') as f:
    f.write(json.dumps([{'key': l} for l in range(10)]))
