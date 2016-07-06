var Config = require('./config');
var strategies = require('./strategies');

var jsonConfig = new Config(strategies.json);
jsonConfig.set('key', 'value');
jsonConfig.save('config.json');


var iniConfig = new Config(strategies.ini);
iniConfig.set('key', 'value');
iniConfig.save('config.ini');
