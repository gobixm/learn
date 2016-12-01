var gulp = require('gulp');
var browserify = require("browserify");
var babelify = require("babelify");
var source = require("vinyl-source-stream");
var gutil = require('gulp-util');
var streamify = require('gulp-streamify');
var uglify = require('gulp-uglify');
var less = require('gulp-less');

gulp.task('build', ['build-less', 'copy-svg', 'copy-index'], () => {
    return browserify({
            entries: './src/index.jsx',
            extensions: ['.jsx'],
            debug: gutil.env.env !== 'production'
        })
        .transform('babelify')
        .bundle()
        .pipe(source('app.js'))
        .pipe(gutil.env.env === 'production' ? streamify(uglify()) : gutil.noop())
        .pipe(gulp.dest('dist'));
});

gulp.task('copy-svg', () => {
    gulp.src('./src/assets/*.svg')
        .pipe(gulp.dest('dist/assets'));
});

gulp.task('copy-index', () => {
    gulp.src('./src/index.html')
        .pipe(gulp.dest('dist'));
});

gulp.task('watch', ['build', 'build-less'], () => {
    gulp.watch(['./src/**/*.jsx', './src/**/*.js', './src/assets/**/*.*'], ['build', 'build-less']);
});

gulp.task('build-less', function() {
    return gulp.src('./src/assets/app.less')
        .pipe(less())
        .pipe(gulp.dest('dist'));
});

gulp.task('default', ['watch']);
