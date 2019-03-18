'use strict';
app.controller('usersController', ['$scope', 'usersService', 'localStorageService',  function ($scope, usersService, localStorageService) {


    $scope.users = [];


    usersService.getusers().then(function (results) {

        var authData = localStorageService.get('authorizationData');

        if (authData) {

            $scope.users = results;
            
        }
    }, function (error) {
        console.log(error.data.message);
    });

}]);