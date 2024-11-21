var gulp = require('gulp'),
    cssmin = require("gulp-cssmin"),
    rename = require("gulp-rename"),
    rimraf = require('rimraf'),
    paths = {
        nodeModuleRoot: 'node_modules/',
        stylesRoot: 'assets/styles',
        scriptsRoot: 'assets/scripts/',
        imagesRoot: 'assets/images',
        webRoot: 'wwwroot/'
    }
const sass = require('gulp-sass')(require('sass'));

gulp.task('default', function () {
    return gulp.src('assets/scss/site.scss')
        .pipe(sass().on('error', sass.logError))
        .pipe(cssmin())
        .pipe(rename({
            //suffix: ".min"
        }))
        .pipe(gulp.dest('wwwroot/css/'));
    });

//main task
gulp.task("watch", function(){
    gulp.watch('assets/scss/*.scss', gulp.series('default'));
});
gulp.task('copy:font-awesome', async  function(){
    // return gulp.src(paths.nodeModuleRoot + 'font-awesome/**/*')
    // .pipe(gulp.dest(paths.webRoot + 'lib/font-awesome/'));
    //  return gulp.src('node_modules/font-awesome/**/*') //Chưa hiểu sao gulp 5 ko dùng đc **
    //  .pipe(gulp.dest('wwwroot/lib/font-awesome'));
     return gulp.src('node_modules/@fortawesome/fontawesome-free/**/*') //Chưa hiểu sao gulp 5 ko dùng đc **
     .pipe(gulp.dest('wwwroot/lib/font-awesome'));

});

gulp.task('clean:font-awesome', function(callback){
    rimraf(paths.webRoot + 'lib/font-awesome', callback)
});

//Main task
//Version cũ
//gulp.task('lib', ['copy:font-awesome']);
//gulp.task('clean',['clean:font-awesome']);

//Vesion +v4.0
gulp.task('lib', gulp.series( 'copy:font-awesome'));
gulp.task('clean', gulp.series('clean:font-awesome'));