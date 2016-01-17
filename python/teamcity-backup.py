#!python3

import sys
import os
import shutil
import argparse
import subprocess
import platform
import re
import logging
import datetime
import urllib.request
import base64
import time

backupregexp = 'TeamCity_Backup_(?P<year>[0-9]{4})(?P<month>[0-9]{2})(?P<day>[0-9]{2})_(?P<hour>[0-9]{2})(?P<minute>[0-9]{2})(?P<second>[0-9]{2})'

def getDateFromFileName(name):
	match = re.search(platform.node().upper()+'_'+backupregexp, name)

	if match:		
		return datetime.datetime(
			int(match.group('year')),
			int(match.group('month')),
			int(match.group('day')),
			int(match.group('hour')),
			int(match.group('minute')),
			int(match.group('second')))

def parseArguments():
	parser = argparse.ArgumentParser(description='Arguments')
	parser.add_argument('--backupDir', type=str, required='true', help = 'path to collect backups from')
	parser.add_argument('--storeShare', type=str, help='path to shared folder in store ')
	parser.add_argument('--storeDir', type=str, required='true', help='path to store backups')
	parser.add_argument('--storeUser', type=str, help='user to connect to share')
	parser.add_argument('--storePassword', type=str, help='password to connect to share')
	parser.add_argument('--backupDays', type=int, help='the amount of days to store backup', default=10)
	parser.add_argument('--tcUser', type=str, help='TeamCity user name')
	parser.add_argument('--tcPassword', type=str, help='TeamCity password')
	parser.add_argument('--tcUrl', type=str, help='TeamCity url', default='http://localhost:8111')
	return parser.parse_args()

def teamcityBackup():
	url = args.tcUrl + '/httpAuth/app/rest/server/backup'		

	username = args.tcUser
	password = args.tcPassword
	p = urllib.request.HTTPPasswordMgrWithDefaultRealm()

	p.add_password(None, url, username, password)

	handler = urllib.request.HTTPBasicAuthHandler(p)
	opener = urllib.request.build_opener(handler)
	urllib.request.install_opener(opener)

	urllib.request.urlopen(url+'?includeConfigs=true&includeDatabase=true&includeBuildLogs=true&fileName=TeamCity_Backup', data=b'')	
	while urllib.request.urlopen(url).read() == b'Running':				
		print('waiting for backup complete')
		time.sleep(1)

def processFiles():
	files = (x for x in os.scandir(args.backupDir) if x.is_file() and re.search(backupregexp, x.name))

	for entry in files:
		print('moving {0}'.format(entry.name))
		shutil.move(entry.path, os.path.join(args.storeDir, platform.node().upper()+'_'+entry.name))

	files = {x:getDateFromFileName(x.name) for x in os.scandir(args.storeDir) if x.name.startswith(platform.node().upper())}

	for entry in files:
		if (datetime.datetime.now() - files[entry]).days > args.backupDays:
			print('deleting {0}'.format(entry.path))
			os.remove(entry.path)	

def netConnect(args):
	if(args.storeShare and args.storeUser and args.storePassword):
		subprocess.run(['net', 'use', args.storeShare, args.storePassword, '/USER:'+args.storeUser])

def netDisconnect(args):
	if(args.storeShare and args.storeUser and args.storePassword):
		subprocess.run(['net', 'use', args.storeDir, '/DELETE'])
try:
	if not os.path.exists('log'):
	    os.makedirs('log')
	logging.basicConfig(filename='log/teamcity-backup.log', level=logging.ERROR, format='%(asctime)s %(message)s')
	logger = logging.getLogger(__name__)

	args = parseArguments()

	if not os.path.exists(args.backupDir):
		logger.error('backup dir "%s" not found', args.backupDir)
		exit()

	netConnect(args);

	if not os.path.exists(args.storeDir):
		logger.error('store dir "%s" not found', args.storeDir)
		exit()

	teamcityBackup();
	processFiles();

	netDisconnect(args);	
except Exception as ex:
	logger.error(ex)
