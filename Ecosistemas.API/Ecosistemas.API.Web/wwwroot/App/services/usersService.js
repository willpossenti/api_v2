'use strict';
app.factory('usersService', ['$http', 'ngAuthSettings', 'authInterceptorService', function ($http, ngAuthSettings, authInterceptorService) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var usersServiceFactory = {};

    
    var _getusers = function () {

        var config = {};
        var headers = authInterceptorService.request(config);

        return $http.get(serviceBase + 'user', { headers: headers }).then(function (results) {

            var arrayResponse = [];
            arrayResponse = angular.fromJson(results.data.result);

       
            return results.data.result;
        });
    };

    usersServiceFactory.getusers = _getusers;

    return usersServiceFactory;

}]);