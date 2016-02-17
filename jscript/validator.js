(function () {
    'use strict';

    var us = require("underscore");

    function validator(message, fun) {
        var f = function () {
            return fun.apply(fun, arguments);
        };
        f.message = message;
        return f;
    }

    function checker() {
        var validators = us.toArray(arguments);
        return function (obj) {
            return us.reduce(validators, function (errs, check) {
                if (check(obj)) {
                    return errs;
                } else {
                    return us.chain(errs).push(check.message).value();
                }
            }, []);
        };
    }

    var numberValidator = validator('is not a number', us.isNumber);
    var maxValidator = validator('is grather than 100', function (obj) {
        if (!us.isNumber(obj)) {
            return true;
        }
        return obj <= 100;
    });
    var numberChecker = checker(numberValidator, maxValidator);
    var errs = us.map([1, 2, 3, 'a', 150, 1], numberChecker);
    console.log(errs);
}());
