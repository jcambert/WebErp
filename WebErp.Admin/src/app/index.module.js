(function(window,angular) {
  'use strict';

  var app=window.app= angular
    .module('webErpAdmin', ['ngAnimate', 'ngCookies', 'ngTouch', 'ngSanitize', 'ngMessages', 'ngAria', 'ngResource', 'ui.router', 'ui.bootstrap', 'toastr','dashboard']);

  
    app.constant('$',window.jQuery);
    
})(window,angular);
