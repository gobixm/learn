var fs = require('fs');
var crypto = require('crypto');

var sha1Stream = crypto.createHash('sha1');
sha1Stream.setEncoding('base64');

var md5Stream = crypto.createHash('md5');
md5Stream.setEncoding('base64');

var input = process.argv[2];
var inputStream = fs.createReadStream(input);

inputStream
    .pipe(sha1Stream)
    .pipe(fs.createWriteStream(input + '.sha1'));

inputStream
    .pipe(md5Stream)
    .pipe(fs.createWriteStream(input + '.md5'));
