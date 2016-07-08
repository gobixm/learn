var zmq = require('zmq');
var split = require('split')();
var randomstring = require("randomstring");

var pump = zmq.socket('push');
pump.bindSync("tcp://*:5002");

while (true) {
    var i = 0;
    var total = 0;   
    var start = process.hrtime();
    var line = randomstring.generate(65535);
    for (i = 0; i < 1000; i++) {
        var msg = {
            "line": line
        };        
        var raw = JSON.stringify(msg);
        pump.send(raw);
        total += line.length;
    }    
    var elapsed = process.hrtime(start)[1] / 1000000000;
    console.log((total / 1000000) / elapsed + " MB/sec")
}

// process.stdin.pipe(split)
//     .on('data', (line) => {
//         console.log('line:' + line);
//         var msg = {
//             "line": line
//         };
//         pump.send(JSON.stringify(msg));
//     });
