var zmq = require('zmq');
var sink = zmq.socket('pull');
sink.bindSync("tcp://*:5001");

sink.on('message', (buffer) => {
    //console.log('worker said: "' + buffer.toString() + '"');
    //console.log('found');
});