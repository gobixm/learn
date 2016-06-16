var combine = require('multipipe');
var fs = require('fs');

var compressAndEncryptStream = require('./cripper').compressAndEncrypt;

combine(
        fs.createReadStream(process.argv[3]),
        compressAndEncryptStream(process.argv[2]),
        fs.createWriteStream(process.argv[3] + '.gz.enc')
    )
    .on('error', function(err) {
        console.log(err);
    });
