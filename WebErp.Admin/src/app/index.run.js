(function() {
  'use strict';

  angular
    .module('webErpAdmin')
    .run(runBlock);

  /** @ngInject */
  function runBlock($log) {

    $log.debug('runBlock end');
  }

})();
