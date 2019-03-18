'use strict';


app.controller('homeController', ['$scope', '$location', 'localStorageService', function ($scope, $location, localStorageService) {


    var body = document.querySelector("body");

    body.style.backgroundImage = '';
    body.className = 'k-header--fixed k-header-mobile--fixed k-subheader--enabled k-subheader--transparent k-aside--enabled k-aside--fixed k-page--loading';

    var authData = localStorageService.get('authorizationData');

    if (!authData) {

        $location.path('/login');
    }


   
}]);