(function () {
    var singleSettingsController = function ($scope, $http, $location, $state, $rootScope, $window, $cookies, usSpinnerService, Idle, Keepalive, $uibModal) {

        
        var singleSettings = function () {
           
            $scope.loadData = false;

        };
        singleSettings();

    };


    fccApp.controller("singleSettingsController", ["$scope", "$http", "$location", "$state", "$rootScope", "$window", "$cookies", "usSpinnerService", "Idle", "Keepalive", "$uibModal", singleSettingsController]);
}())