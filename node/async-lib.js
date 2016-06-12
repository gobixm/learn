var async = require('async');
var process = require('process');

function makePrintCb(data) {
    return function(callback) {
        process.nextTick(function() {
            console.log(data);
            callback();
        })
    };
}

function makePrint(data) {
    return function(callback) {
        console.log(data);
    };
}

async.series([
        makePrintCb(1),
        makePrintCb(2)
    ],
    makePrint('done'));


async.eachSeries([1, 2, 3], function(item, callback) {
    process.nextTick(function() {
        console.log('item' + item);
        callback();
    });
}, makePrint('all done'));

async.each([1, 2, 3], function(item, callback) {
    process.nextTick(function() {
        console.log('item' + item);
        callback();
    });
}, makePrint('all done'));

var q = async.queue(function(task, callback) {
    task(callback);
}, 2);

q.push(function() {
    process.nextTick(function(callback) {
            console.log('queue');
            callback();
        },
        function() {
            console.log('cb');
        });
})
q.push(function() {
    process.nextTick(function(callback) {
            console.log('queue2');
            callback();
        },
        function() {
            console.log('cb2');
        });
})
