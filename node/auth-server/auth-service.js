var db = require('./db');
var users = db.sublevel('users');

exports.login = function(username, password, callback) {
    users.get(username, function(err, user) {
        console.log('login:' + user);
        callback();
    });
}

exports.checkToken = function(token, callback) {
	console.log('check login: '+token);
	callback();
}
