'use strict';
app.controller('indexController', ['$scope', '$location', 'authService',  function ($scope, $location, authService) {

  
    $scope.logOut = function () {
        authService.logOut();
        $location.path('/home');
    };


    //$scope.changeBackground = {
    //    'background-image': 'url(Content/media/misc/bg_1.jpg)'
    //};

 

    //var authData = localStorageService.get('authorizationData');
    //console.log(authData);

    $scope.authentication = authService.authentication;

}]);


