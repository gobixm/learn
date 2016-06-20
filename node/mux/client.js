var childProcess = require('child_process');
var net = require('net');

function multiplexChannels(sources, destionation) {
    var totalChannels = sources.length;
    for (var i = 0; i < totalChannels; i++) {
        sources[i]
            .on('readable', function (i) {
                var chunk;
                while ((chunk = this.read()) !== null) {
                    var outBuffer = new Buffer(1 + 4 + chunk.length);
                    outBuffer.writeUInt8(i, 0);
                    outBuffer.writeUInt32BE(chunk.length, 1);
                    chunk.copy(outBuffer, 5);
                    console.log('sending packet to channel ' + i);
                    destionation.write(outBuffer);
                }
            }.bind(sources[i], i))
            .on('end', function () {
                if (--totalChannels === 0) {
                    destionation.end();
                }
            });
    }
}

var socket = net.connect(3000, function () {
    var child = childProcess.fork(process.argv[2],
        process.argv.slice(3),
        {silent: true});
    multiplexChannels([child.stdout, child.stderr], socket);
});
