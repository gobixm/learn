var process = require('process')
var TaskQueue = require('./task-queue');

var arr = [1, 2, 3, 4, 5]

function concurrent(callback) {
    var completed = 0;
    arr.forEach(function(item) {
        process.nextTick(function() {
            console.log(item);
            if (++completed === arr.length) {
                callback();
            }
        });
    });
}

concurrent(function() {
    console.log('all done');
});


function makeTask(index) {
    return function(callback) {
        process.nextTick(function() {
            console.log('task' + index)
            callback();
        });
    }
}
var tasks = [makeTask(1), makeTask(2), makeTask(3)];
var concurrency = 2,
    running = 0,
    completed = 0,
    index = 0;

function next() {
    while (running < concurrency && index < tasks.length) {
        var task = tasks[index++];
        task(function() {
            completed++;
            running--;
            if (completed === tasks.length) {
                return finish();
            }
            next();
        });
        running++;
    }
}
next();

function finish() {
    console.log('finish');
}

var queue = new TaskQueue(2);

queue.pushTask(function(done) {
    process.nextTick(function() {
        console.log('done1');
        done()
    });
});
queue.pushTask(function(done) {
    process.nextTick(function() {
        console.log('done2');
        done()
    });
});
