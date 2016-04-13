(function (window, angular) {
    'use strict';
    var app = window.weberp ;

    app.constant('$', window.jQuery);
    app.constant('_', _);
    app.constant('HubName', 'WebErpHub');

    app.constant('LOCALES', {
        'locales': {
            // 'ru_RU': 'Русский',
            'fr_FR': 'Francais'
        },
        'preferredLocale': 'fr_FR'
    });

    app.constant('EndPoints', (function () {
        var partial_dir = 'http://localhost:8081/api/';

        return {
            BASE_DIR: partial_dir,
            ARTICLES: partial_dir + 'article',
        }
    })());

    app.constant('Partials', (function () {
        var partial_dir = '/Home/';
        var ext = '';//'.partial.html';
        return {
            BASE_DIR: partial_dir,
            DASHBOARD: partial_dir + 'dashboard' + ext,
            HOME: partial_dir + 'home' + ext,
            ADD_THING: partial_dir + 'add.things' + ext,
            UPDATE_SETTINGS: partial_dir + 'settings' + ext,
            THINGS: partial_dir + 'things' + ext,
            INPUTS: partial_dir + 'inputs' + ext,
            OUTPUTS: partial_dir + 'outputs' + ext
        }
    })());

    app.config(['$locationProvider','$stateProvider', '$urlRouterProvider', '$logProvider','$injector', 'Partials', function ($locationProvider,$stateProvider, $urlRouterProvider, $log,$injector, $partials) {
        $log.debugEnabled(true);

        if ($injector.has('$translateProvider')) {
            /* Translation*/
            $translateProvider.useMissingTranslationHandlerLog();
            $translateProvider.useStaticFilesLoader({
                //prefix: 'resources/locale-',// path to translations files
                prefix: 'Locale/locale-',
                suffix: '.json'
            });
            $translateProvider.preferredLanguage('fr_FR');
            $translateProvider.useLocalStorage();
            /* Translation */

        }
        $urlRouterProvider.otherwise("/home");
        $stateProvider
            .state('home', {
                url: '/home',
                templateUrl: $partials.HOME
            });
           

        $locationProvider.html5Mode(false).hashPrefix('!');

        console.info('Application is configured');
    }]);


})(window, angular);