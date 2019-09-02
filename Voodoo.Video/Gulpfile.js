"use strict";


const gulp = require("gulp");
const cache = require("gulp-cached");


function copyNodeModules(cb) {
    gulp.src('node_modules/lodash/**')
        .pipe(cache('node_modules'))
        .pipe(gulp.dest('wwwroot/vendor/lodash'));

    gulp.src('node_modules/gridstack/dist/**')
        .pipe(cache('node_modules'))
        .pipe(gulp.dest('wwwroot/vendor/gridstack/dist'));

    gulp.src('node_modules/requirejs/**')
        .pipe(cache('node_modules'))
        .pipe(gulp.dest('wwwroot/vendor/requirejs'));
    
    gulp.src('node_modules/underscore/**')
        .pipe(cache('node_modules'))
        .pipe(gulp.dest('wwwroot/vendor/underscore'));

    gulp.src('node_modules/pjax/pjax**.js')
        .pipe(cache('node_modules'))
        .pipe(gulp.dest('wwwroot/vendor/pjax'));
    
    gulp.src('node_modules/tabulator-tables/dist/**')
        .pipe(cache('node_modules'))
        .pipe(gulp.dest('wwwroot/vendor/tabulator-tables/dist'));
    cb();
}


exports.copynodemodules = copyNodeModules;
