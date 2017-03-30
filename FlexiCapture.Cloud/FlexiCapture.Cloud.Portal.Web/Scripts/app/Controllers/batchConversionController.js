(function () {
    var batchFileConversionController = function ($scope, $http, $location, $state, $rootScope, $window, $cookies, usSpinnerService, Idle, Keepalive, $uibModal) {

        
        var batchFileConversion = function () {
           
            $scope.loadData = false;

        };
        batchFileConversion();

    };


    fccApp.controller("batchFileConversionController", ["$scope", "$http", "$location", "$state", "$rootScope", "$window", "$cookies", "usSpinnerService", "Idle", "Keepalive", "$uibModal", batchFileConversionController]);
}())