(function (window, angular) {
    'use strict';

    var q= angular.module('quotation');

    q.controller('quotationCtrl', quotationController);
    q.controller('materialCtrl', materialController);
    q.controller('tablePreferencesCtrl', tablePreferencesController)
    /** @ngInject */
    function quotationController($log, NgTableParams) {
        var self = this;
        var data = [
                        { name: "Moroni", age: 50 },
                        { name: "Tiancum", age: 43 },
                        { name: "Jacob", age: 27 },
                        { name: "Nephi", age: 29 },
                        { name: "Enos", age: 34 }
        ];
        self.tableParams = new NgTableParams({}, { getData: function (params) { return data;} });
       

    }

    /** @ngInject */
    function materialController($log, $scope,partials,$uibModal, NgTableParams) {
        var self = $scope;

        self.materials = [{ id: 0, nuance: 'acier', designation: 'XC10<3', prix: 0.58, densite: 8, remarque: '' },
                          { id: 1, nuance: 'acier', designation: 'XC10>3', prix: 0.55, densite: 8, remarque: '' },
                          { id: 2, nuance: 'acier', designation: 'XC10>10', prix: 0.70, densite: 8, remarque: '' },
                          { id: 3, nuance: 'inox', designation: '304L', prix: 2.3, densite: 8, remarque: '' }
        ];
        self.cols = [{
            field: "id",
            title: "id",
            show: false,
            getValue: simpleValue
        }, {
            field: "nuance",
            title: "Nuance",
            show: true,
            getValue: simpleValue
            //getValue: interpolatedValue,
            //interpolateExpr: $interpolate("<em class='text-danger'>{{ user.age | number:1}}</em>")
        }, {
            field: "designation",
            title: "Designation",
            sortable: "designation",
            show: true,
            getValue: simpleValue
            // getValue: evaluatedValue,
            // valueFormatter: "currency:'$'"
        }];
        self.tableParams = new NgTableParams({
            sorting: { designation: "asc" }
        }, {
            getData: function (params) { return self.materials; }
            // dataset: self.materials
        });

        function simpleValue($scope, row) {
            return row[this.field];
        }

        self.wantModifyTableSettings = function (size) {
            size = size | 'lg';
            //alert('toto');
            var modalInstance = $uibModal.open({
                animation: $scope.animationsEnabled,
                templateUrl: partials.TABLESETTINGS,
                size: size,
                /*resolve: {
                    items: function () {
                        return $scope.items;
                    }
                }*/
            });

            modalInstance.result.then(function (item) {
                //var selected = selectedItem;
                $log.log('Want add thing with id:' + item.id)
                //$thingsapi.save(item);
            }, function () {
                $log.info('Modal dismissed at: ' + new Date());
            });

        }

        

    }
        
    /** @ngInject */
    function tablePreferencesController($scope,$uibModalInstance,$rootScope, ngTableDefaults ) {
        var self = $scope;
        self.pageSizes = [
           { label: "5 10 20", sizes: [5, 10, 20] },
           { label: "10 20 50", sizes: [10, 20, 50] }
        ];
        self.defaultSettings = {
            counts: self.pageSizes[0].sizes,
            defaultSort: "asc",
            filterOptions: {
                filterComparator: true
            }
        };
        self.initialSorting = [
            { label: "Id ASC", value: { id: "asc"} },
            { label: "Id DESC", value: { id: "desc"} },
            { label: "Name ASC", value: { name: "asc"} },
            { label: "Name DESC", value: { name: "desc"} }
            ];
        self.initialParams = {
            page: 1,
            count: 10,
            sorting: undefined
        };
      
        
        self.ok = function () {
            applyPreferences();
            $uibModalInstance.close();
        };

        self.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };

        function applyPreferences(){
            ngTableDefaults.settings = angular.extend({}, ngTableDefaults.settings, self.defaultSettings);
            ngTableDefaults.params = angular.extend({}, ngTableDefaults.params, self.initialParams);
            $rootScope.$broadcast("demoNgTableDefaultsChanged", ngTableDefaults);
            self.ok()
        }
    }

})(window, angular);
