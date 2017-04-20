(function () {
    var singleLibraryController = function ($scope,$interval, $http, $location, $state, $rootScope, $window, $cookies, usSpinnerService, Idle, Keepalive, $uibModal, documentsHttpService) {

        var data = [];
        var url = $$ApiUrl + "/documents";


        var timer;
        if (!timer) {
            timer = $interval(function () {
                //console.log('Start silence!');
                documentsHttpService.getToDocumentsSilent($http, $scope, $state, data, url, usSpinnerService);
            }
                , 5000);
        }

        $scope.killtimer = function () {
            if (angular.isDefined(timer)) {
                $interval.cancel(timer);
                timer = undefined;
            }
        };

        $scope.$on('$destroy', function () {
            $scope.killtimer();
        });




        var singleLibrary = function () {
            documentsHttpService.getToDocuments($http, $scope, $state, data, url, usSpinnerService);
            $scope.loadData = false;


        };
        singleLibrary();

    };


    fccApp.controller("singleLibraryController", ["$scope","$interval", "$http", "$location", "$state", "$rootScope", "$window", "$cookies", "usSpinnerService", "Idle", "Keepalive", "$uibModal", "documentsHttpService", singleLibraryController]);
}())