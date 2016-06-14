var ToFileStream = require('./to-filestream');
var stream = new ToFileStream();
stream.write({ path: "file1.txt", content: 'file1' });
stream.write({ path: "file2.txt", content: 'file2' });
stream.write({ path: "file3.txt", content: 'file3' });
stream.end(function(){
	console.log('all created');
});
