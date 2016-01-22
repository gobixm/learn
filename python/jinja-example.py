#!python3
import csv
from jinja2 import Environment, FileSystemLoader

rows = []

with open('data.csv') as csvfile:
	reader = csv.DictReader(csvfile)
	headers = reader.fieldnames;	
	for row in reader:
		rows.append([row[x] for x in headers])

	

env = Environment(loader=FileSystemLoader('./'))
template = env.get_template('template.html')

with open("output.html", "w") as fh:
    fh.write(template.render(headers=headers, data=rows))