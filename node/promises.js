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

var tasks = [ptask1, ptask2, ptask3];

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

var promise = q.Promise.resolve();
tasks.forEach(function(task) {
    promise = promise.then(function() {
        return task();
    });
});
promise.then(function() {
    console.log('all completed');
})


var promise = tasks.reduce(function(prev, task) {
    return prev.then(function() {
        return task();
    });
}, q.Promise.resolve());

var promises = [1,2,3].map(function(item){
	return new q.Promise(function(resolve, reject){
		process.nextTick(function(){
			console.log(item);
			resolve();
		});
	});
});

q.Promise.all(promises);
