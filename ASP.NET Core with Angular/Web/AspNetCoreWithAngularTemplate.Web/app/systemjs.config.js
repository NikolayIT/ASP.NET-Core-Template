'use strict';

(function (global) {
    // where to look for things
    var map = {
        '@angular': 'lib/@angular',
        '@angular/common/http': 'lib/@angular/common/bundles/common-http.umd.min.js',
        'app': 'app',
        'rxjs': 'lib/rxjs',
        'tslib': 'lib/tslib/tslib.js',
        'zone.js': 'lib/zone.js/dist'
    };

    // how to load when no filename and/or no extension
    var packages = {
        'app': { main: 'main', defaultExtension: 'js' },
        'rxjs': { defaultExtension: 'js' },
        'zone.js': { main: 'zone', defaultExtension: 'js' }
    };

    [
        'animations',
        'core',
        'common',
        'compiler',
        'forms',
        'platform-browser',
        'platform-browser-dynamic',
        'router'
    ].forEach(function (packageName) {
        packages['@angular/' + packageName] = {
            main: 'bundles/' + packageName + '.umd.min.js',
            defaultExtension: 'js'
        };
    });

    System.config({
        map: map,
        packages: packages
    });
})(this);
