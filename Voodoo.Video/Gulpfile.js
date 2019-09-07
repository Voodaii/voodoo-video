"use strict";


const gulp = require("gulp");
const cache = require("gulp-cached");


function copyNodeModules(cb) {
    // JQuery
    gulp.src("node_modules/jquery/dist/jquery.min.js")
        .pipe(cache("node_modules"))
        .pipe(gulp.dest("wwwroot/vendor/jquery/dist"));
    
    // JQuery UI
    gulp.src("node_modules/jquery-ui-dist/jquery-ui.min.js")
        .pipe(cache("node_modules"))
        .pipe(gulp.dest("wwwroot/vendor/jquery-ui-dist"));
    gulp.src("node_modules/jquery-ui-dist/jquery-ui.min.css")
        .pipe(cache("node_modules"))
        .pipe(gulp.dest("wwwroot/vendor/jquery-ui-dist"));
    
    // Copy fontawesome style.
    gulp.src("node_modules/@fortawesome/fontawesome-free/css/all.css")
        .pipe(cache("node_modules"))
        .pipe(gulp.dest("wwwroot/vendor/fontawesome/css"));
    // Copy fontawesome fonts.
    gulp.src("node_modules/@fortawesome/fontawesome-free/webfonts/**")
        .pipe(cache("node_modules"))
        .pipe(gulp.dest("wwwroot/vendor/fontawesome/webfonts"));

    // Copy gridstacks
    gulp.src('node_modules/lodash/lodash.min.js')
        .pipe(cache('node_modules'))
        .pipe(gulp.dest('wwwroot/vendor/lodash'));
    
    // Copy gridstacks
    gulp.src('node_modules/gridstack/dist/**')
        .pipe(cache('node_modules'))
        .pipe(gulp.dest('wwwroot/vendor/gridstack/dist'));

//     gulp.src('node_modules/pjax/pjax**.js')
//         .pipe(cache('node_modules'))
//         .pipe(gulp.dest('wwwroot/vendor/pjax'));
//    
    // Copy tabulator
    gulp.src('node_modules/tabulator-tables/dist/**')
        .pipe(cache('node_modules'))
        .pipe(gulp.dest('wwwroot/vendor/tabulator-tables/dist'));
    cb();
}


exports.copynodemodules = copyNodeModules;
