(function (window, angular) {
    'use strict';
    var app = window.weberp;

    app.service('Hub', ['$','$rootScope', '$log', function ($,$rootScope, $log) {
        //This will allow same connection to be used for all Hubs
        //It also keeps connection as singleton.
        var globalConnections = [];

        function initNewConnection(options) {
            var connection = null;
            if (options && options.rootPath) {
                connection = $.hubConnection(options.rootPath, { useDefaultPath: false });
            } else {
                connection = $.hubConnection();
            }

            connection.logging = (options && options.logging ? true : false);
            return connection;
        }

        function getConnection(options) {
            var useSharedConnection = !(options && options.useSharedConnection === false);
            if (useSharedConnection) {
                return typeof globalConnections[options.rootPath] === 'undefined' ?
                globalConnections[options.rootPath] = initNewConnection(options) :
                globalConnections[options.rootPath];
            }
            else {
                return initNewConnection(options);
            }
        }

        return function (hubName, options) {
            var Hub = this;
            Hub.connected = false;
            Hub.connection = getConnection(options);
            Hub.proxy = Hub.connection.createHubProxy(hubName);
            
            Hub.on = function (event, fn) {
                Hub.proxy.on(event, fn);
            };

            Hub.on('', function () { console.log('ADDTHING'); });
            Hub.invoke = function (method, args) {
                //if(Hub.connected)
                return Hub.proxy.invoke.apply(Hub.proxy, arguments)
            };
            Hub.disconnect = function () {
                Hub.connection.stop();
            };
            Hub.connect = function () {
                var startOptions = {};
                if (options.transport) startOptions.transport = options.transport;
                if (options.jsonp) startOptions.jsonp = options.jsonp;
                if (angular.isDefined(options.withCredentials)) startOptions.withCredentials = options.withCredentials;
                return Hub.connection.start(startOptions);
            };

            if (options && options.listeners) {
                Object.getOwnPropertyNames(options.listeners)
                .filter(function (propName) {
                    return typeof options.listeners[propName] === 'function';
                })
                    .forEach(function (propName) {
                        Hub.on(propName, options.listeners[propName]);
                    });
            }
            if (options && options.methods) {
                angular.forEach(options.methods, function (method) {
                    Hub[method] = function () {
                        var args = $.makeArray(arguments);
                        args.unshift(method);
                        return Hub.invoke.apply(Hub, args);
                    };
                });
            }
            if (options && options.queryParams) {
                Hub.connection.qs = options.queryParams;
            }
            if (options && options.errorHandler) {
                Hub.connection.error(options.errorHandler);
            }
            if (options && options.stateChanged) {
                Hub.connection.stateChanged(options.stateChanged);
            }

            //Adding additional property of promise allows to access it in rest of the application.
            if (options.autoConnect === undefined || options.autoConnect) {
                Hub.promise = Hub.connect();
            }
            $log.log(Hub);
            return Hub;
        };

        app.service('WebErpHub', ['$rootScope', 'Hub', 'HubName', '$timeout', '$log', function ($rootScope, Hub, HubName, $timeout, $log) {
            //declaring the hub connection
            var hub = new Hub(HubName, {
                useSharedConnection: true,
                //client side methods
                /* listeners: {
                     'lockEmployee': function (id) {
                         var employee = find(id);
                         employee.Locked = true;
                         $rootScope.$apply();
                     },
                     'unlockEmployee': function (id) {
                         var employee = find(id);
                         employee.Locked = false;
                         $rootScope.$apply();
                     }
                 },*/

                //server side methods
                // methods: ['deleteThing'],

                //query params sent on initial connection
                /* queryParams: {
                     'token': 'exampletoken'
                 },*/

                //handle connection error
                errorHandler: function (error) {
                    console.error(error);
                },

                //specify a non default root
                //rootPath: '/api

                stateChanged: function (state) {
                    switch (state.newState) {
                        case $.signalR.connectionState.connecting:
                            $log.log(HubName + ' connecting');
                            //your code here
                            break;
                        case $.signalR.connectionState.connected:
                            $log.log(HubName + ' connected');
                            this.connected = true;
                            //your code here
                            break;
                        case $.signalR.connectionState.reconnecting:
                            $log.log(HubName + ' reconnecting');
                            //your code here
                            break;
                        case $.signalR.connectionState.disconnected:
                            $log.log(HubName + ' disconnecting');
                            //your code here
                            connected = false;
                            break;
                    }
                }
            });


            /*var edit = function (employee) {
                hub.lock(employee.Id); //Calling a server method
            };
            var done = function (employee) {
                hub.unlock(employee.Id); //Calling a server method
            }*/
            var hubStart = function (init) {
                return hub.connect().done(init);
            };

            var updateStatus = function () {
                hub.invoke('UpdateStatus');
            };

            var isConnected = function () {
                return hub.connected;
            }
            return {
                on: hub.on,
                start: hubStart,
                updateStatus: updateStatus,
                isConnected: isConnected
                /*editEmployee: edit,
                doneWithEmployee: done*/
            };
        }]);

    }]);

})(window, angular);