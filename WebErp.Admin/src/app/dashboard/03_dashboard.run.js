(function(angular) {
  'use strict';
 var dashboard =  angular.module('dashboard');
  dashboard.run(runBlock);

  /** @ngInject */
  function runBlock($log,$$window) {
    angular.element(".right_col").css("min-height", $$window.height());
    $$window.resize(function () {
        angular.element(".right_col").css("min-height", $$window.height());
    });
  //  $log.debug('Dashboard is running');
  
  }

})(angular);
