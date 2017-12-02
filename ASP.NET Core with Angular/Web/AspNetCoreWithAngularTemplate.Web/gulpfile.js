'use strict';

/*
    Imports
*/
var gulp = require('gulp'),
    ts = require('gulp-typescript'),
    tsconfig = require('tsconfig-glob'),
    merge = require('merge'),
    fs = require('fs'),
    del = require('del'),
    deleteEmpty = require('delete-empty'),
    path = require('path'),
    sourcemaps = require('gulp-sourcemaps'),
    concat = require('gulp-concat'),
    uglify = require('gulp-uglify'),
    rename = require('gulp-rename'),
    minifyCss = require('gulp-clean-css'),
    sysBuilder = require('systemjs-builder'),
    htmlMinifier = require('html-minifier'),
    gulpHtmlMinifier = require('gulp-html-minifier'),
    htmlReplace = require('gulp-html-replace'),
    inlineNg2Template = require('gulp-inline-ng2-template'),
    sequence = require('gulp-sequence'),
    useref = require('gulp-useref'),
    replace = require('gulp-replace');

/*
    Paths
*/
var paths = {
    src: './app/',
    dest: './wwwroot/',
    npm: './node_modules/'
};

paths = {
    core: paths,
    bundle: {
        root: paths.dest,
        config: paths.dest + 'systemjs.config.js',
        main: 'app/main',
        modules: ['account', 'user', 'shared'], // Angular modules
        dest: paths.dest + 'app.min.js'
    },
    index: {
        src: paths.src + 'index.html',
        dest: paths.dest,
        path: paths.dest + 'index.html'
    },
    version: {
        name: '.' + new Date().getTime().toString(),
        files: ['app.min.js', 'css/app.min.css', 'lib/vendor.min.js']
    },
    ts: {
        src: paths.src + '**/*.ts',
        dest: paths.dest + 'app/',
        config: paths.src + 'tsconfig.json',
        def: paths.dest + 'definitions/'
    },
    html: {
        src: [paths.src + 'components/**/*.html', '!**/index.html'],
        dest: paths.dest + 'app/components/'
    },
    css: {
        src: paths.src + 'content/css/**/*',
        dest: paths.dest + 'css/'
    },
    img: {
        src: paths.src + 'content/img/**/*',
        dest: paths.dest + 'img/'
    },
    media: {
        src: paths.src + 'content/media/**/*',
        dest: paths.dest + 'media/'
    },
    lib: {
        js: paths.dest + 'lib',
        css: paths.dest + 'lib',
        img: paths.dest + 'lib',
        fonts: paths.dest + 'lib',
        lib: {
            src: [paths.src + 'content/**/*', '!favicon.ico'],
            dest: paths.dest
        }
    },
    angularMain: {
        src: paths.src + 'main.ts',
        dest: paths.src
    }
};

var jsSys = [
    'core-js/client/shim.min.js',
    'zone.js/dist/zone.min.js',
    'reflect-metadata/Reflect.js',
    'systemjs/dist/system.src.js'
];

var jsLibs = [
    'bootstrap/dist/js/bootstrap.js',
    'jquery/dist/jquery.min.js'
];

/*
    Libs tasks
*/
gulp.task('setup-libs-app', function (done) {
    var appJs = jsSys.map(function (lib) {
        return 'node_modules/**/' + lib;
    }).concat([
        'node_modules/**/@angular/core/bundles/core.umd.min.js',
        'node_modules/**/@angular/common/bundles/common.umd.min.js',
        'node_modules/**/@angular/common/bundles/common-http.umd.min.js',
        'node_modules/**/@angular/compiler/bundles/compiler.umd.min.js',
        'node_modules/**/@angular/platform-browser/bundles/platform-browser.umd.min.js',
        'node_modules/**/@angular/platform-browser-dynamic/bundles/platform-browser-dynamic.umd.min.js',
        'node_modules/**/@angular/router/bundles/router.umd.min.js',
        'node_modules/**/@angular/forms/bundles/forms.umd.min.js',

        'node_modules/**/rxjs/**/*.js',

        'node_modules/**/tslib/tslib.js'
    ]);

    return gulp.src(appJs).pipe(gulp.dest(paths.lib.js));
});

gulp.task('setup-libs-js', function (done) {
    var js = jsLibs.map(function (lib) {
        return 'node_modules/**/' + lib;
    });

    return gulp
        .src(js)
        .pipe(rename(function (path) {
            var libName = path.dirname.split('\\')[0];
            path.dirname = libName + '\\js';
        }))
        .pipe(gulp.dest(paths.lib.js));
});

gulp.task('setup-libs-css', function (done) {
    var css = [
        'node_modules/**/bootstrap/dist/css/bootstrap.css'
    ];

    return gulp
        .src(css)
        .pipe(rename(function (path) {
            var libName = path.dirname.split('\\')[0];
            path.dirname = libName + '\\css';
        }))
        .pipe(gulp.dest(paths.lib.css));
});

gulp.task('setup-libs-fonts', function (done) {
    var fonts = [
        'node_modules/**/bootstrap/dist/fonts/glyphicons-halflings-regular.eot',
        'node_modules/**/bootstrap/dist/fonts/glyphicons-halflings-regular.svg',
        'node_modules/**/bootstrap/dist/fonts/glyphicons-halflings-regular.ttf',
        'node_modules/**/bootstrap/dist/fonts/glyphicons-halflings-regular.woff',
        'node_modules/**/bootstrap/dist/fonts/glyphicons-halflings-regular.woff2'
    ];

    return gulp
        .src(fonts)
        .pipe(rename(function (path) {
            var libName = path.dirname.split('\\')[0];
            path.dirname = libName + '\\fonts';
        }))
        .pipe(gulp.dest(paths.lib.fonts));
});

gulp.task('setup-libs', ['setup-libs-app', 'setup-libs-js', 'setup-libs-css', 'setup-libs-fonts']);

/*
    Artefacts tasks
*/
gulp.task('move-artefacts-lib', function (done) {
    return gulp.src(paths.lib.lib.src).pipe(gulp.dest(paths.lib.lib.dest));
});

gulp.task('move-artefacts-systemjs', function (done) {
    return gulp.src(paths.core.src + 'systemjs.config.js').pipe(gulp.dest(paths.core.dest));
});

gulp.task('move-artefacts-css', function (done) {
    return gulp.src(paths.css.src).pipe(gulp.dest(paths.css.dest));
});

gulp.task('move-artefacts-html', function (done) {
    return gulp.src(paths.html.src).pipe(gulp.dest(paths.html.dest));
});

gulp.task('move-artefacts-img', function (done) {
    return gulp.src(paths.img.src).pipe(gulp.dest(paths.img.dest));
});

gulp.task('move-artefacts-media', function (done) {
    return gulp.src(paths.media.src).pipe(gulp.dest(paths.media.dest));
});

gulp.task('move-artefacts-immutable', function (done) {
    return gulp.src(paths.core.src + 'content/favicon.ico').pipe(gulp.dest(paths.core.dest));
});

gulp.task('move-artefacts', [
    'move-artefacts-lib',
    'move-artefacts-systemjs',
    'move-artefacts-css',
    'move-artefacts-html',
    'move-artefacts-img',
    'move-artefacts-media',
    'move-artefacts-immutable'
]);

/*
    Typescript tasks
*/
gulp.task('compile-typescript', function (done) {
    var tsProject = ts.createProject(paths.ts.config);

    return gulp.src([paths.ts.src])
        .pipe(tsProject(), undefined, ts.reporter.fullReporter())
        .js.pipe(gulp.dest(paths.ts.dest));
});

/* 
    Enable angular production mode
*/
gulp.task('enable-angular-prod-mode', function (done) {
    return gulp.src(paths.angularMain.src)
        .pipe(replace('// enableProdMode();', 'enableProdMode();'))
        .pipe(gulp.dest(paths.angularMain.dest));
});

gulp.task('disable-angular-prod-mode', function (done) {
    return gulp.src(paths.angularMain.src)
        .pipe(replace('enableProdMode();', '// enableProdMode();'))
        .pipe(gulp.dest(paths.angularMain.dest));
});

/*
    Watch tasks
*/
function reportFileChanged(reporter, file) {
    console.log(reporter + ': file changed: ' + file.path.substring(file.path.lastIndexOf('\\') + 1));
}

gulp.task('watch-css', function (done) {
    return gulp.watch(paths.css.src, function (obj) {
        if (obj.type === 'changed') {
            gulp.src(obj.path, { base: paths.core.src + 'content/css/' })
                .pipe(gulp.dest(paths.css.dest));

            reportFileChanged('watch-css', obj);
        }
    });
});

gulp.task('watch-html', function (done) {
    return gulp.watch(paths.html.src, function (obj) {
        if (obj.type === 'changed') {
            gulp.src(obj.path, { base: paths.core.src + 'components/' })
                .pipe(gulp.dest(paths.html.dest));

            reportFileChanged('watch-html', obj);
        }
    });
});

gulp.task('watch-index-html-copy', function (done) {
    return gulp.watch(paths.index.src, function (obj) {
        if (obj.type === 'changed') {
            gulp.src(obj.path, { base: paths.core.src })
                .pipe(gulp.dest(paths.core.dest));

            reportFileChanged('watch-index-html', obj);
        }
    });
});

gulp.task('watch-index-html', ['watch-index-html-copy', 'index-dev']);

gulp.task('watch-ts', function (done) {
    return gulp.watch(paths.ts.src, ['compile-typescript']);
});

gulp.task('watch', ['watch-ts', 'watch-css', 'watch-html', 'watch-index-html']);

/*
    Clean tasks
*/
gulp.task('clean', function (done) {
    return del([paths.core.dest]);
});

/*
    Templates inlining tasks
*/
gulp.task('inline', function (done) {
    del.sync(paths.core.dest + 'app/components/**/*.html');

    var minifyTemplate = function (path, ext, file, cb) {
        try {
            var minifiedFile = htmlMinifier.minify(file,
                {
                    collapseWhitespace: true,
                    caseSensitive: true,
                    removeComments: true
                });

            cb(null, minifiedFile);
        } catch (err) {
            cb(err);
        }
    };

    var tsProject = ts.createProject(paths.ts.config);

    return gulp.src([paths.ts.src])
        .pipe(inlineNg2Template({
            base: '/app',
            useRelativePaths: true,
            templateProcessor: minifyTemplate
        }))
        .pipe(tsProject(), undefined, ts.reporter.fullReporter())
        .js.pipe(gulp.dest(paths.ts.dest));
});

/*
    Bundle tasks
*/
gulp.task('bundle-app', function () {
    var builder = new sysBuilder(paths.bundle.root, paths.bundle.config);

    var mergedPath = paths.bundle.main;
    paths.bundle.modules.forEach(function (moduleName) {
        mergedPath += ` + (app/components/${moduleName}/${moduleName}.module - ${paths.bundle.main})`;
    });

    return builder
        .bundle(mergedPath, paths.bundle.dest, { minify: true, sourceMaps: false, encodeNames: false })
        .then(function () {
            return del([paths.core.dest + 'app', paths.lib.js + '/@angular', paths.lib.js + '/rxjs']);
        });
});

gulp.task('bundle', sequence('bundle-app'));

/*
    Minify tasks
*/
gulp.task('minify-js', function (done) {
    return gulp.src([paths.core.dest + '**/*.js', '!' + paths.core.dest + '**/*.min.js'])
        .pipe(uglify().on('error', function (err) {
            console.log(err);
        }))
        .pipe(gulp.dest(paths.core.dest));
});

gulp.task('minify-css', function (done) {
    return gulp.src([paths.core.dest + '**/*.css', '!' + paths.core.dest + '**/*.min.css'])
        .pipe(minifyCss())
        .pipe(gulp.dest(paths.core.dest));
});

gulp.task('minify', sequence('minify-js', 'minify-css'));

/*
    Index tasks
*/
gulp.task('index-dev', function (done) {
    var preHeadLines = [
        '<script src="systemjs.config.js"></script>'
    ];

    var preBodyLines = [
        '<script type="text/javascript">System.import("app").catch(console.log.bind(console));</script>'
    ];

    return gulp.src(paths.index.src)
        .pipe(replace('</head>', preHeadLines.join(' ') + ' </head>'))
        .pipe(replace('</body>', preBodyLines.join(' ') + ' </body>'))
        .pipe(gulp.dest(paths.index.dest));
});

gulp.task('index-release', function (done) {
    var preHeadLines = [
        '<script src="systemjs.config.js"></script>'
    ];

    var preBodyLines = [
        '<script src="app.min.js"></script>',
        '<script type="text/javascript">System.import("app").catch(console.log.bind(console));</script>'
    ];

    return gulp.src(paths.index.src)
        .pipe(replace('</head>', preHeadLines.join(' ') + ' </head>'))
        .pipe(replace('</body>', preBodyLines.join(' ') + ' </body>'))
        .pipe(gulp.dest(paths.index.dest));
});

/*
    Concat tasks
*/
gulp.task('concat-impl', function (done) {
    return gulp.src(paths.index.path)
        .pipe(useref())
        .pipe(gulp.dest(paths.index.dest));
});

gulp.task('concat-clean', function (done) {
    return del([paths.css.dest + '**/*.css', '!' + paths.css.dest + 'app.min.css']);
});

gulp.task('concat', sequence('concat-impl', 'concat-clean'));

/*
    Version tasks
*/
gulp.task('version-files', function (done) {
    var files = paths.version.files.map(function (f) {
        return paths.core.dest + '**/' + f;
    });

    return gulp.src(files)
        .pipe(rename({ suffix: paths.version.name }))
        .pipe(gulp.dest(paths.core.dest));
});

gulp.task('version-refs', function (done) {
    var task = gulp.src(paths.index.path);

    paths.version.files.forEach(function (f) {
        task.pipe(replace(f, f.substr(0, f.lastIndexOf('.')) + paths.version.name + f.substr(f.lastIndexOf('.'))));
    });

    return task.pipe(gulp.dest(paths.index.dest));
});

gulp.task('version-clean', function (done) {
    var files = paths.version.files.map(function (f) {
        return paths.core.dest + '**/' + f;
    });

    return del(files);
});

gulp.task('version', sequence('version-files', 'version-refs', 'version-clean'));

/*
    Index Uglify tasks
*/
gulp.task('index-uglify', function (done) {
    return gulp.src(paths.index.path)
        .pipe(gulpHtmlMinifier({
            collapseWhitespace: true,
            caseSensitive: true,
            removeComments: true
        }))
        .pipe(gulp.dest(paths.index.dest));
});

/*
    Clean after release tasks
*/
gulp.task('clean-after-release', function (done) {
    deleteEmpty.sync(paths.core.dest);
    done();
});

/*
    Main tasks
*/
gulp.task('release', sequence(
    'clean',
    'setup-libs',
    'move-artefacts',
    'enable-angular-prod-mode',
    'compile-typescript',

    'inline',
    'bundle',
    'disable-angular-prod-mode',
    'minify',
    'index-release',
    'concat',
    'version',
    'index-uglify',
    'clean-after-release'
));

gulp.task('build', sequence(
    'clean',
    'setup-libs',
    'move-artefacts',
    'compile-typescript',
    'index-dev'
));

gulp.task('dev', sequence('build', 'watch'));

gulp.task('default', ['release']);
