var gulp = require('gulp');
var babelify = require("babelify");
var webpack = require("gulp-webpack");
var source = require("vinyl-source-stream");
var gutil = require('gulp-util');
var streamify = require('gulp-streamify');
var uglify = require('gulp-uglify');
var less = require('gulp-less');
var concat = require('gulp-concat');
var plumber = require('gulp-plumber');
var notify = require('gulp-notify');
var buffer = require('vinyl-buffer');
var source = require('vinyl-source-stream');

gulp.task('build', ['build-less', 'copy-svg', 'copy-index'], () => {
    var errorMsg = function() {
        let args = Array.prototype.slice.call(arguments);
        // Send error to notification center with gulp-notify

        notify.onError({
            title: "Compile Error",
            message: "<%= error %>"
        }).apply(this, args);
        // Keep gulp from hanging on this task
        this.emit('end');
    };
    return gulp.src('./src/index.jsx')
        .pipe(webpack({
            target: 'node',
            resolve: {
                extensions: ['', '.js', '.jsx']
            },
            devtool: 'source-map',
            output: {
                filename: "app.js",
                sourceMapFilename: "app.js.map"
            },
            module: {
                loaders: [{
                    test: /\.(js|jsx)$/,
                    exclude: /(node_modules|bower_components)/,
                    loader: "babel-loader",
                    query: {
                        presets: ['es2015', 'stage-3', 'react'],
                        plugins: ['syntax-async-functions']
                    }
                }]
            }
        }))
        .on('error', errorMsg)
        .pipe(plumber())
        .pipe(buffer())
        .pipe(gulp.dest('dist'));
});

gulp.task('copy-svg', () => {
    gulp.src('./src/**/*.svg')
        .pipe(gulp.dest('dist/assets'));
});

gulp.task('copy-index', () => {
    gulp.src('./src/index.html')
        .pipe(gulp.dest('dist'));
});

gulp.task('watch', ['build', 'build-less'], () => {
    gulp.watch(['./src/**/*.jsx', './src/**/*.js', './src/**/*.less'], ['build', 'build-less']);
});

gulp.task('build-less', function() {
    return gulp.src('./src/**/*.less')
        .pipe(concat('app.less'))
        .pipe(less())
        .pipe(gulp.dest('dist'));
});

gulp.task('default', ['watch']);
