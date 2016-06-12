var process = require('process');

function task1(callback) {
    process.nextTick(function() {
        task2(callback);
    });
}

function task2(callback) {
    process.nextTick(function() {
        task3(callback);
    });
}

function task3(callback) {
    callback(null, 'done');
}

task1(function(err, data) {
    console.log(data);
});


var arr = [1, 2, 3]

function iterate(index) {	
    process.nextTick(function() {
        if (index === arr.length) {
            return;
        }        
        console.log(arr[index]);
        iterate(++index);
    });
}

iterate(0);

function iterateSeries(collection, iteratorCallback, finalCallback) {
    var index = 0;

    function iterate(index) {
        process.nextTick(function() {
            if (index === collection.length) {
                finalCallback();
                return;
            }
            iteratorCallback(collection[index]);
            iterate(++index);
        });
    }
    iterate(0);
}

iterateSeries(arr, function(item) {
        console.log(item);
    },
    function() {
        console.log('final');
    })
