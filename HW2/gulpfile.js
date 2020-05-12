/// <binding BeforeBuild='min' Clean='clean' ProjectOpened='auto' />
"use strict";

//����ʹ�õ��� gulp ���
const gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-clean-css"),
    rename = require("gulp-rename"),
    uglify = require("gulp-uglify"),
    changed = require("gulp-changed");


//���� wwwroot �µĸ��ļ����·��
const paths = {
    root: "./wwwroot/",
    css: './wwwroot/css/',
    js: './wwwroot/js/',
    lib: './wwwroot/lib/'
};

//css
paths.cssDist = paths.css + "**/*.css";//ƥ������ css ���ļ�����·��
paths.minCssDist = paths.css + "**/*.min.css";//ƥ������ css ��Ӧѹ������ļ�����·��
paths.concatCssDist = paths.css + "app.min.css";//�����е� css ѹ����һ�� css �ļ����·��

//js
paths.jsDist = paths.js + "**/*.js";//ƥ������ js ���ļ�����·��
paths.minJsDist = paths.js + "**/*.min.js";//ƥ������ js ��Ӧѹ������ļ�����·��
paths.concatJsDist = paths.js + "app.min.js";//�����е� js ѹ����һ�� js �ļ����·��


//ʹ�� npm ���ص�ǰ�������
const libs = [
    { name: "jquery", dist: "./node_modules/jquery/dist/**/*.*" },
    { name: "popper", dist: "./node_modules/popper.js/dist/**/*.*" },
    { name: "bootstrap", dist: "./node_modules/bootstrap/dist/**/*.*" },
    {
        name: "showdown", dist:"./node_modules/showdown/dist/**/*.*"
    }
];

//���ѹ������ļ�
gulp.task("clean:css", done => rimraf(paths.minCssDist, done));
gulp.task("clean:js", done => rimraf(paths.minJsDist, done));

gulp.task("clean", gulp.series(["clean:js", "clean:css"]));

//�ƶ� npm ���ص�ǰ��������� wwwroot ·����
gulp.task("move", done => {
    libs.forEach(function (item) {
        gulp.src(item.dist)
            .pipe(gulp.dest(paths.lib + item.name + "/dist"));
    });
    done()
});

//ÿһ�� css �ļ�ѹ������Ӧ�� min.css
gulp.task("min:css", () => {
    return gulp.src([paths.cssDist, "!" + paths.minCssDist], { base: "." })
        .pipe(rename({ suffix: '.min' }))
        .pipe(changed('.'))
        .pipe(cssmin())
        .pipe(gulp.dest('.'));
});

//�����е� css �ļ��ϲ����ѹ���� app.min.css ��
gulp.task("concatmin:css", () => {
    return gulp.src([paths.cssDist, "!" + paths.minCssDist], { base: "." })
        .pipe(concat(paths.concatCssDist))
        .pipe(changed('.'))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});

//ÿһ�� js �ļ�ѹ������Ӧ�� min.js
gulp.task("min:js", () => {
    return gulp.src([paths.jsDist, "!" + paths.minJsDist], { base: "." })
        .pipe(rename({ suffix: '.min' }))
        .pipe(changed('.'))
        .pipe(uglify())
        .pipe(gulp.dest('.'));
});

//�����е� js �ļ��ϲ����ѹ���� app.min.js ��
gulp.task("concatmin:js", () => {
    return gulp.src([paths.jsDist, "!" + paths.minJsDist], { base: "." })
        .pipe(concat(paths.concatJsDist))
        .pipe(changed('.'))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("min", gulp.series(["min:js", "min:css"]));
gulp.task("concatmin", gulp.series(["concatmin:js", "concatmin:css"]));


//�����ļ��仯���Զ�ִ��
gulp.task("auto", () => {
    gulp.watch(paths.css, gulp.series(["min:css", "concatmin:css"]));
    gulp.watch(paths.js, gulp.series(["min:js", "concatmin:js"]));
});
gulp.task('lib', function () {     //����npm����web root��
    gulp.src(paths.node_modules_libs).pipe(gulp.dest(paths.webroot + 'lib'))
});