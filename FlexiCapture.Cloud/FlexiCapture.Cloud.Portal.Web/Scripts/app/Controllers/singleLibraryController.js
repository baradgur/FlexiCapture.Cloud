(function () {
    var singleLibraryController = function ($scope, $http, $location, $state, $rootScope, $window, $cookies, usSpinnerService, Idle, Keepalive, $uibModal, documentsHttpService) {
        var data = [];
        var url = $$ApiUrl + "/documents";
        var singleLibrary = function () {
            documentsHttpService.getToDocuments($http, $scope, $state, data, url, usSpinnerService);
            $scope.loadData = false;

        };
        singleLibrary();

    };


    fccApp.controller("singleLibraryController", ["$scope", "$http", "$location", "$state", "$rootScope", "$window", "$cookies", "usSpinnerService", "Idle", "Keepalive", "$uibModal","documentsHttpService", singleLibraryController]);
}())