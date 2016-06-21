function Profiler(label) {
    this.label = label;
    this.lastTime = null;
}

Profiler.prototype.start = function () {
    this.lastTime = process.hrtime();
};

Profiler.prototype.end = function () {
    var diff = process.hrtime(this.lastTime);
    console.log('timer ' + this.label + 'took: ' + diff[0] + ' seconds, ' + diff[1] + 'nanoseconds');
};

module.exports = function (label) {
    if (process.env.NODE_ENV === 'development') {
        return new Profiler(label);
    } else {
        return {
            start: function () {
            },
            end: function () {
            }
        }
    }
};
