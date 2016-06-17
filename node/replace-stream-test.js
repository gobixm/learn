var ReplaceStream = require('./replace-stream');

var rs = new ReplaceStream(',', ' and');
rs.on('data', function(chunk) {
    process.stdout.write(chunk);
});

rs.setEncoding("utf-8")
rs.write('coff');
rs.write('ee, ci');
rs.write('garrets');
rs.end();
