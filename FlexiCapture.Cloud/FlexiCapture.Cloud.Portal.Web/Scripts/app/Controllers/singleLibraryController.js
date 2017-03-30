(function () {
    var singleLibraryController = function ($scope, $http, $location, $state, $rootScope, $window, $cookies, usSpinnerService, Idle, Keepalive, $uibModal) {

        
        var singleLibrary = function () {
           
            $scope.loadData = false;

        };
        singleLibrary();

    };


    fccApp.controller("singleLibraryController", ["$scope", "$http", "$location", "$state", "$rootScope", "$window", "$cookies", "usSpinnerService", "Idle", "Keepalive", "$uibModal", singleLibraryController]);
}())