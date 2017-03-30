(function () {
    var singleProfileController = function ($scope, $http, $location, $state, $rootScope, $window, $cookies, usSpinnerService, Idle, Keepalive, $uibModal) {

        
        var singleProfile = function () {
           
            $scope.loadData = false;

        };
        singleProfile();

    };


    fccApp.controller("singleProfileController", ["$scope", "$http", "$location", "$state", "$rootScope", "$window", "$cookies", "usSpinnerService", "Idle", "Keepalive", "$uibModal", singleProfileController]);
}())