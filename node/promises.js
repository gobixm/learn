var promise = require('./promises-lib');
var process = require('process');
var q = require('q');

function task1(callback) {	
    process.nextTick(function() {
        console.log('task1');    
        callback(null, 'res1');
    })
}

function task2(callback) {
    process.nextTick(function() {
        console.log('task2');
        callback(null, 'res2');        
    })
}

function task3(callback) {
    process.nextTick(function() {
        console.log('task3');
        callback(null, 'res3');        
    })
}

var ptask1 = promise.promisify(task1);
var ptask2 = promise.promisify(task2);
var ptask3 = promise.promisify(task3);

ptask1()
    .then(function(res) {
    	console.log(res);
        return ptask2();
    })
    .then(function(res) {
    	console.log(res);
    	return ptask3();
    })
    .then(function(res) {
    	console.log(res);
    });
