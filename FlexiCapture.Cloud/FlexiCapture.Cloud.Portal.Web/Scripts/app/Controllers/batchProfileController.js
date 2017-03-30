(function () {
    var batchProfileController = function ($scope, $http, $location, $state, $rootScope, $window, $cookies, usSpinnerService, Idle, Keepalive, $uibModal) {

        
        var batchProfile = function () {
           
            $scope.loadData = false;

        };
        batchProfile();

    };


    fccApp.controller("batchProfileController", ["$scope", "$http", "$location", "$state", "$rootScope", "$window", "$cookies", "usSpinnerService", "Idle", "Keepalive", "$uibModal", batchProfileController]);
}())