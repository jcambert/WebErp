(function () {
    'use strict';

    angular
      .module('quotation')
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
                        state:'quotation',
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

})();

