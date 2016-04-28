(function(angular) {
  'use strict';

  angular
    .module('webErpAdmin')
    .controller('MainController', MainController);

  /** @ngInject */
  function MainController(menu,$timeout, webDevTec, toastr,$translate) {
    var vm = this;

    vm.awesomeThings = [];
    vm.classAnimation = '';
    vm.creationDate = 1460101446071;
    vm.showToastr = showToastr;

    activate();

    function activate() {
      getWebDevTec();
      $timeout(function() {
        vm.classAnimation = 'rubberBand';
      }, 4000);
    }

    function showToastr() {
      toastr.info('Fork <a href="https://github.com/Swiip/generator-gulp-angular" target="_blank"><b>generator-gulp-angular</b></a>');
      vm.classAnimation = '';
    }

    function getWebDevTec() {
      vm.awesomeThings = webDevTec.getTec();

      angular.forEach(vm.awesomeThings, function(awesomeThing) {
        awesomeThing.rank = Math.random();
      });
    }
    
    vm.appTitle="Web Erp";
    vm.appIcon='pagelines';
    vm.user={photo:'http://lorempixel.com/32/32/people/1/',firstname:"Jean-Christophe",lastname:"Ambert", fullname:'Ambert Jean-Christophe'};
    
    vm.menuSections = menu.getItems();
      /*
     vm.menuSections=[{
                title:'GENERAL',
                menus:[
                     {
                        icon:'home',
                        text:'Accueil',
                        childs:[
                            {
                                id:0,
                                text:'temp',
                                state:'home'
                            },
                            {
                                id:1,
                                text:'temp',
                                link:'home'
                            },
                        ]
                    },
                    {
                        icon:'building-o',
                        text:'Articles',
                        tooltip:'article-management-tooltip',
                        childs:[
                            {
                                id:0,
                                text:'tous',
                                state:'article.list',
                               //tooltip:'Tous les articles'
                                
                            },
                            {
                                id:1,
                                text:'creer',
                                link:'state4'
                            },
                        ]
                    }
                ]
            },{
                title:'LIVE ON',
                menus:[
                    {
                        icon:'bug',
                        text:'Additional Pages',
                        childs:[{
                            text:'temp',
                            label:{
                                variation:'success',
                                text:10
                            }
                            
                        },{
                            text:'temp'
                        }
                        ]
                    }
                ]
            }];

      */
  }
})(angular);
