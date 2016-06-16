var concatFiles = require('./concat-files');
concatFiles(process.argv[2], process.argv.slice(3), function() {
    console.log('complete ' + process.argv[2]);
});
