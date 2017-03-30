(function () {
    var batchSettingsController = function ($scope, $http, $location, $state, $rootScope, $window, $cookies, usSpinnerService, Idle, Keepalive, $uibModal) {

        
        var batchSettings = function () {
           
            $scope.loadData = false;

        };
        batchSettings();

    };


    fccApp.controller("batchSettingsController", ["$scope", "$http", "$location", "$state", "$rootScope", "$window", "$cookies", "usSpinnerService", "Idle", "Keepalive", "$uibModal", batchSettingsController]);
}())