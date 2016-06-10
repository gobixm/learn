'use strict'

var process = require('process');

function deffered(){
	console.log('deffered');
}

console.log('start');
process.nextTick(deffered);
console.log('finish');

console.log('start');
setImmediate(deffered);
console.log('finish');
