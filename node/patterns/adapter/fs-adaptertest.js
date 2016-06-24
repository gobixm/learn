var levelup = require('level');
var fsAdapter = require('./fs-adapter');
var db = levelup('./fsdb', { valueEncoding: 'binary' });
var fs = fsAdapter(db);

fs.writeFile('file.txt', 'file content', function() {
    fs.readFile('file.txt', { encoding: 'utf-8' }, function(err, res) {
        console.log(res);
    });
});
