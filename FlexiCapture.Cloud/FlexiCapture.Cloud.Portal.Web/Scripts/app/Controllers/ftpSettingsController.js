(function () {
    var ftpSettingsController = function ($scope, $http, $location, $state, $rootScope, $window, $cookies, usSpinnerService, Idle, Keepalive, $uibModal) {

        
        var ftpSettings = function () {
           
            $scope.loadData = false;

        };
        ftpSettings();

    };


    fccApp.controller("ftpSettingsController", ["$scope", "$http", "$location", "$state", "$rootScope", "$window", "$cookies", "usSpinnerService", "Idle", "Keepalive", "$uibModal", ftpSettingsController]);
}())