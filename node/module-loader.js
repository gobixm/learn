'use strict'

var fs = require('fs');

function loadModule(filename, module, require) {
    var wrappedSrc = '(function(module, exports, require) {' +
        fs.readFileSync(filename, 'utf8') +
        '}) (module, module.exports, require);';
    eval(wrappedSrc);
}

var require = function(moduleName) {
    console.log('require ' + moduleName);

    var module = {
        exports: {}
    };

    loadModule(moduleName + '.js', module, require);
    return module.exports;
};

var simple = require('modules/simple');
console.log(simple.whoami())
