var level = require('level');
var sublevel = require('level-sublevel');

module.exports = sublevel(
    level('db', { valueEncoding: 'json' })
);
