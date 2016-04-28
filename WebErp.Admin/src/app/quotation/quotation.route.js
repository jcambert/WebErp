(function () {
    'use strict';

    angular
      .module('quotation')
      .config(routerConfig);

    /** @ngInject */
    function routerConfig($stateProvider, $urlRouterProvider) {
        $stateProvider
          .state('quotation', {
              url: '/quotation',
              templateUrl: 'app/quotation/main.html',
              controller:'quotationCtrl'
              //controllerAs: 'main'
          });

        $urlRouterProvider.otherwise('/');
    }

})();