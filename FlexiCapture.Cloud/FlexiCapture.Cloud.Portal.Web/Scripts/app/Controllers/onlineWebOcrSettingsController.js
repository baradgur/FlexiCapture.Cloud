var onlineWebOcrSettingsController = function($scope, $http, $location, $state, $rootScope, $window, $cookies, usSpinnerService, Idle, Keepalive, $uibModal) {


    var onlineWebOcrSettings = function() {

        $scope.loadData = false;

    };
    onlineWebOcrSettings();

};

fccApp.controller("onlineWebOcrSettingsController", ["$scope", "$http", "$location", "$state", "$rootScope", "$window", "$cookies", "usSpinnerService", "Idle", "Keepalive", "$uibModal", onlineWebOcrSettingsController]);