'use strict'

var process = require('process');

process.on('uncaughtException', function(err){
	console.error('uncaughted ' + err.message);
	process.exit(1);
})

throw new Error('error');