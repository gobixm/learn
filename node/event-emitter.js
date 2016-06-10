'use strict'

var EventEmitter = require('events').EventEmitter;
var process = require('process');
var util = require('util')

function notifier() {
    var ee = new EventEmitter();
    process.nextTick(function() {
        ee.emit('some_event', 1);
        ee.emit('some_event', 2);
        ee.emit('some_event', 3);
    });
    return ee;
}

function AnotherNotifier() {
    EventEmitter.call(this);
}
util.inherits(AnotherNotifier, EventEmitter);

AnotherNotifier.prototype.run = function() {
    var self = this;
    process.nextTick(function() {
        self.emit('other_event', 1);
        self.emit('other_event', 2);
        self.emit('other_event', 3);
    });
    return this;
}

notifier()
    .on('some_event', function(data) {
        console.log('on ' + data);
    })
    .once('some_event', function(data) {
        console.log('once ' + data);
    })

var other = new AnotherNotifier();
other
    .run()
    .on('other_event', function(data) {
        console.log('other ' + data);
    });
