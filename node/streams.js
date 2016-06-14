var chance = require('chance').Chance();
var http = require('http');
var RandomStream = require('./random-stream');

var randomSteam = new RandomStream();
randomSteam.on('readable', function() {
    var chunk;
    while ((chunk = randomSteam.read()) !== null) {
        console.log(chunk.toString());
    }
});

http.createServer(function(req, res) {
    res.writeHead(200, { 'Content-Type': 'text/plain' });

    function generateMore() {
        while (chance.bool({ likelihood: 95 })) {
            var shouldContinue = res.write(chance.string({ length: (16 * 1024) }));
            if (!shouldContinue) {
                console.log('backpressure');
                return res.once('drain', generateMore);
            }
        }
        res.end('\nend\n', function() {
            console.log('all send');
        });        
    }
    generateMore();
}).listen(8080, function() {
    console.log('listening...');
}).on('error', function(err) {
    console.log(err);
});
