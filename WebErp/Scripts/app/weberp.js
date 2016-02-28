(function (window,angular) {
    'use strict';
    var weberp = window.weberp = angular.module('weberp', []);
    
    weberp.directive('wSidebarMenu', [function () {
        return {
            restrict: 'E',
            replace: true,
            transclude: true,
            template:'<ul class="nav side-menu"><li ng-repeat="item in items"><a> <i class="fa fa-{{item.icon}}"></i>{{item.text}}</a><w-menu-item items="{{item.items}}"></w-menu-item></li></ul>'
        };
    }]);

    weberp.directive('wMenuItem', [function () {
        return {
            restrict: 'E',
            replace: true,
            transclude: true,
            require: '^wSidebarMenu',
            template:'<ul class="nav" ng-class="{\'child_menu\': items && items.length>0}"><li ng-repeat="item in items"><w-menu-subitem state="{{item.state || \'\'}}" link="{{item.link || \'\'}}">{{item.text}} </w-menu-subitem></li></ul>'
        };
    }]);

    weberp.directive('wMenuSubitem', [function () {
        return {
            restrict: 'E',
            replace: true,
            require: '^wMenuItem',
            transclude:true,
            template:function($element,$attrs){
                if (angular.isDefined($attrs.state) && eval($attrs.state) !== "")
                    return '<a ui-sref="' + $attrs.state + '" ng-transclude></a>';
                if (angular.isDefined($attrs.link) && eval($attrs.link) !== "")
                    return '<a ng-click="' + $attrs.link + '" ng-transclude></a>';
                return '<a ng-transclude></a>';
            },
           
        };
    }]);
})(window,angular);