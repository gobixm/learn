var fromArray = require('from2-array');
var through = require('through2');
var fs = require('fs');

function concatFiles(destionation, files, callback) {
    var destStream = fs.createWriteStream(destionation);

    fromArray.obj(files)
        .pipe(through.obj(function(file, encoding, done) {
            var src = fs.createReadStream(file);
            src.pipe(destStream, { end: false });
            src.on('end', function() {
                done();
            });
        }))
        .on('finish', function() {
            destStream.end();
            callback();
        });
}

module.exports = concatFiles;
