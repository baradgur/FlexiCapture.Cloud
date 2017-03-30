(function () {
    var emailSettingsController = function ($scope, $http, $location, $state, $rootScope, $window, $cookies, usSpinnerService, Idle, Keepalive, $uibModal) {

        
        var emailSettings = function () {
           
            $scope.loadData = false;

        };
        emailSettings();

    };


    fccApp.controller("emailSettingsController", ["$scope", "$http", "$location", "$state", "$rootScope", "$window", "$cookies", "usSpinnerService", "Idle", "Keepalive", "$uibModal", emailSettingsController]);
}())