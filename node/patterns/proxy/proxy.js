function createProxy(subject) {
    var proto = Object.getPrototypeOf(subject);

    function Proxy(subject) {
        this.subject = subject;
    }

    Proxy.prototype = Object.create(proto);

    Proxy.prototype.foo = function () {
        return this.subject.foo();
    };

    Proxy.prototype.bar = function () {
        return this.subject.bar.apply(this.subject, arguments);
    };

    return new Proxy(subject);
}

function Subject(){
    this.foo = function () {
        console.log('foo')
    };

    this.bar = function(value) {
        console.log(value)
    }
}

var proxy = createProxy(new Subject());

proxy.foo();
proxy.bar('bar');
