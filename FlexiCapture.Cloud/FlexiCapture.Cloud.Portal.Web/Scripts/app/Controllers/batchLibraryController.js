(function () {
    var batchLibraryController = function ($scope, $http, $location, $state, $rootScope, $window, $cookies, usSpinnerService, Idle, Keepalive, $uibModal) {

        
        var batchLibrary = function () {
           
            $scope.loadData = false;

        };
        batchLibrary();

    };


    fccApp.controller("batchLibraryController", ["$scope", "$http", "$location", "$state", "$rootScope", "$window", "$cookies", "usSpinnerService", "Idle", "Keepalive", "$uibModal", batchLibraryController]);
}())