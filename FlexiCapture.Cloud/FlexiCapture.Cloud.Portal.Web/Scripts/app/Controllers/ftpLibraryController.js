(function () {
    var ftpLibraryController = function ($scope, $http, $location, $state, $rootScope, $window, $cookies, usSpinnerService, Idle, Keepalive, $uibModal) {

        
        var ftpLibrary = function () {
           
            $scope.loadData = false;

        };
        ftpLibrary();

    };


    fccApp.controller("ftpLibraryController", ["$scope", "$http", "$location", "$state", "$rootScope", "$window", "$cookies", "usSpinnerService", "Idle", "Keepalive", "$uibModal", ftpLibraryController]);
}())