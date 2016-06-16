var ParallelStream = require('./parallel-stream');
var LimitedParallelStream = require('./limited-parallel-stream');

var ps = new ParallelStream(function(chunk, encoding, done) {
    setTimeout(function() {
        console.log(chunk);
        done();
    }, Math.floor(Math.random() * 1000));
});

for (var i = 0; i < 100; ++i) {
    ps.write('chunk' + i);
}

var lps = new LimitedParallelStream(10, function(chunk, encoding, done) {
    setTimeout(function() {
        console.log(chunk);
        done();
    }, Math.floor(Math.random() * 1000));
});

for (var i = 0; i < 100; ++i) {
    lps.write('limitedchunk' + i);
}
