(function () {
    var singleLibraryController = function ($scope, $interval, $http, $location, $state, $rootScope, $window, $cookies, usSpinnerService, Idle, Keepalive, $uibModal, documentsHttpService) {

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

        $scope.gotoDeleteSelectedPositions = function () {
            var positionsToDelete = $('#table').bootstrapTable('getSelections');
            if (positionsToDelete.length > 0) {
                var deleteData = [];
                for (var i = 0; i < positionsToDelete.length; i++) {
                    deleteData.push({
                        'Id': positionsToDelete[i].Id,
                        'TaskId': positionsToDelete[i].taskId
                    });
                };
                documentsHttpService.deleteSelectedPositions($http, $scope, data, deleteData, url, usSpinnerService);
            }
            else {
                BootstrapDialog.alert({
                    title: 'Warning',
                    message: 'There were no documents selected to delete!',
                    type: BootstrapDialog.TYPE_WARNING
                });
            }
        }

    };


    fccApp.controller("singleLibraryController", ["$scope", "$interval", "$http", "$location", "$state", "$rootScope", "$window", "$cookies", "usSpinnerService", "Idle", "Keepalive", "$uibModal", "documentsHttpService", singleLibraryController]);
}())