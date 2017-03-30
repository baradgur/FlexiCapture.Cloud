(function () {
    var emailLibraryController = function ($scope, $http, $location, $state, $rootScope, $window, $cookies, usSpinnerService, Idle, Keepalive, $uibModal) {

        
        var emailLibrary = function () {
           
            $scope.loadData = false;

        };
        emailLibrary();

    };


    fccApp.controller("emailLibraryController", ["$scope", "$http", "$location", "$state", "$rootScope", "$window", "$cookies", "usSpinnerService", "Idle", "Keepalive", "$uibModal", emailLibraryController]);
}())