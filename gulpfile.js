'use strict';
var gulp = require('gulp');
var inlinesource = require('gulp-inline-source');

gulp.task('inlinesource', function () {
    return gulp.src('./Sensusplaylist.Test/Coverage/*.htm')
        .pipe(inlinesource({attribute: false}))
        .pipe(gulp.dest('./Sensusplaylist.Test/Coverage-Inline'));
});