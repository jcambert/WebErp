(function(window,angular) {
  'use strict';
 var dashboard = window.dashboard ;
 dashboard.constant('LOCALES', {
    'locales': {
        // 'ru_RU': 'Русский',
        'fr_FR': 'Francais'
    },
    'preferredLocale': 'fr_FR'
    });
 
 dashboard.constant('$partials',(function(){
       var partial_dir='app/dashboard/partials/';
       
       return{
           BASE_DIR:partial_dir,
           DASHBOARD: partial_dir + 'dashboard.directive.html',
           SIDEBAR:partial_dir + 'wSidebar.directive.html'
           
       }
    })());
  dashboard.constant('$',window.jQuery);
  
  dashboard.constant('$$window', /** @ngInject */ function($window){
      
       
       return angular.element($window)
           
       
    }());
    
 dashboard.config(config);
  /** @ngInject */
  function config($logProvider, $translateProvider) {
    // Enable log
    $logProvider.debugEnabled(true);
    
    /*configure translation */
    $translateProvider.useMissingTranslationHandlerLog();
    $translateProvider.useStaticFilesLoader({
      prefix: 'assets/i18n/locale-',
      suffix: '.json'
   });
   
   $translateProvider.preferredLanguage('fr_FR');
  }

})(window,angular);
