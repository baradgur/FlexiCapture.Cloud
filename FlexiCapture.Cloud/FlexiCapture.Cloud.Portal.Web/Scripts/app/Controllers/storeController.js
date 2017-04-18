(function () {
    var storeController = function ($scope, $http, $location, $state, $rootScope, $window, $cookies, usSpinnerService, Idle, Keepalive, $uibModal) {

        
        var store = function () {
           
            $scope.loadData = false;

        };
        store();

    };


    fccApp.controller("storeController", ["$scope", "$http", "$location", "$state", "$rootScope", "$window", "$cookies", "usSpinnerService", "Idle", "Keepalive", "$uibModal", storeController]);
}())