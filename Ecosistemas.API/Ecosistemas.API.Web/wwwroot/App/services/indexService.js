'use strict';
app.factory('indexService', function () {

    var indexServiceFactory = {};

    var _backgroundLogin = function () {

            return { 'background-image': 'url(Content/media/misc/bg_1.jpg)' };
        
    };



    indexServiceFactory.backgroundLogin = _backgroundLogin;

});