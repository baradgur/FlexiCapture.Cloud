var logController = function ($scope, $http, $location, $state, $window, $uibModal, $stateParams, usSpinnerService, logHttpService) {
    var url = $$ApiUrl + "/Log";

    $scope.loading = true;
    var data = [];

    $scope.loadLogData = function () {
        logHttpService.getLog($http, $scope, data, url, usSpinnerService);
    }
    
    $scope.loadLogData();


};
fccApp.controller("logController", ["$scope", "$http", "$location", "$state", "$window", "$uibModal", "$stateParams", "usSpinnerService", "logHttpService", logController]);