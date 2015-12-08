var request = require('request');

function WorldTrip(foo) {
    return new Promise(foo);
}

WorldTrip((resolve, reject) => {
        request
            .get('http://gooogle.com')
            .on('response', resolve)
            .on('error', reject);
    })
    .then((response) => {
        console.log('content-type:' + response.headers['content-type']);
        return response;
    })
    .then((response) => {
        return new Promise((resolve, reject) => {
            console.log("BODY:");
            response.setEncoding('utf8');
            var buff = '';
            response
                .on('data', function(data) {
                    if (buff.length < 1000) {
                        buff += data;
                    }
                })
                .on('end', () => {
                    console.log(buff.substring(0, 1000));
                    resolve(response);
                });
        });
    })
    .then((response) => {
        throw 'custom error';
    })
    .catch((error) => {
        console.log('some error occured: ' + error);
    });
