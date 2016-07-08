var zmq = require('zmq');
var argv = require('minimist')(process.argv.slice(2));

var fromPump = zmq.socket('pull');
var toSink = zmq.socket('push');

fromPump.connect('tcp://localhost:5002');
toSink.connect('tcp://localhost:5001');
var word = argv.word;

var total = 0;
var start;

fromPump.on('message', function(buffer) {
        if (total > 100000000) {        	
            if (start) {
                var elapsed = process.hrtime(start)[1] / 1000000000;
                console.log((total / 1000000) / elapsed + " MB/sec")                
                
            }
            total = 0;
            start = process.hrtime();
        }                
        var msg = JSON.parse(buffer);
        var line = msg.line;                
        // if (line.search(word) !== -1) {            
        //     toSink.send('found "' + word + '" in line "' + line + '"');
        // }
        total += line.length;
    })
    .on('error', (err) => {
        console.log(err);
    });
