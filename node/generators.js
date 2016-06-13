function* makeGenerator() {
    yield 'uno';
    yield 'dos';
    yield 'tres';
}

function* twoWayGenerator() {
    var input = yield null;
    input = yield 'uno ' + input;
    input = yield 'dos ' + input;
    input = yield 'tres ' + input;
}

var gen = makeGenerator();

console.log(gen.next());
console.log(gen.next());
console.log(gen.next());
console.log(gen.next());

var twoway = twoWayGenerator();

console.log(twoway.next());
console.log(twoway.next(1));
console.log(twoway.next(2));
console.log(twoway.next(3));
twoway.throw(new Error());
