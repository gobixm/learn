var level = require('level');
var levelSubscribe = require('./level-subscribe');

var db = level(__dirname + '/db', { valueEncoding: 'json' });
db = levelSubscribe(db);

db.subscribe({ doctype: 'message', language: 'en' }, function(k, val) {
    console.log(val);
});

db.put('1', { doctype: 'message', text: 'content', language: 'en' });
db.put('2', { doctype: 'other', document: 'document' });
