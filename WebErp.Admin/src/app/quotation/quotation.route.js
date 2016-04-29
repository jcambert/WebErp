(function () {
    'use strict';

    angular
      .module('quotation')
      .config(routerConfig);

    /** @ngInject */
    function routerConfig($stateProvider, $urlRouterProvider,partials) {
        $stateProvider
          .state('quotation', {
              url: '/quotation',
              abstract:true,
              template: '<div class="row" ui-view></div>',
              //controller:'quotationCtrl',
              //controllerAs: 'vm'
          })
            .state('quotation.list', {
                url: '/list',
                templateUrl: partials.MAIN,//'app/quotation/main.html',
                controller: 'quotationCtrl',
                controllerAs: 'vm'

            }) 
            .state('quotation.material', {
                url: '/material',
                templateUrl: partials.MATERIAL,// 'app/quotation/material.html',
                controller: 'materialCtrl',
                //controllerAs: 'vm'
            })
        ;

        $urlRouterProvider.otherwise('/');
    }

})();