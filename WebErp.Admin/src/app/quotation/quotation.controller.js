(function (window, angular) {
    'use strict';

    var q= angular.module('quotation');

    q.controller('quotationCtrl', quotationController);

    /** @ngInject */
    function quotationController($scope, NgTableParams) {
        var self = this;
        var data = [{ name: "Moroni", age: 50 } /*,*/];
        self.tableParams = new NgTableParams({}, { dataset: data });
    }

})(window, angular);
