(function () {
    'use strict';

    angular
      .module('quotation')
      .constant('partials',(partials()))
      .config(config)
    ;

    /** @ngInject */
    function config(menuProvider) {
        menuProvider.add({
            title: 'Chiffrage',
            menus: [
                {
                    icon: 'bug',
                    text: 'Gestion',
                    state: 'quotation',
                    childs: [{
                        text: 'list',
                        state:'quotation.list',
                        label: {
                            variation: 'success',
                            text: 10
                            }

                        }, {
                        text: 'materials',
                        state: 'quotation.material',
                        /*label: {
                            variation: 'success',
                            text: 10
                        }*/

                    }, {
                            text: 'temp'
                        }
                    ]
                }
            ]
        });
    }

    /** @ngInject */
    function partials () {
        var partial_dir = 'app/quotation/';
        var ext = '.html';
        return {
            BASE_DIR: partial_dir,
            MAIN: partial_dir + 'main' + ext,
            MATERIAL: partial_dir + 'material' + ext,
            TABLESETTINGS: partial_dir + 'tableSettings' + ext
        }
    }
})();

