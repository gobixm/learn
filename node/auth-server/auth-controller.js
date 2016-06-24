var authService = require('./auth-service');

exports.login = function(request, result, next) {
    authService.login(request.body.username, request.body.password,
        function(err, result) {
            console.log('login done');
            next();
        });
}

exports.checkToken = function(request, result, next) {
    authService.checkToken(request.query.token,
        function(err, result) {
        	console.log('check done');
        	next();
        });
}
