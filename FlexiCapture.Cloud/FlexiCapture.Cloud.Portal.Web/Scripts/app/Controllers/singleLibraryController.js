function actionFormatterSingleLibrary(value, row, index) {
    return [
        '<button class="btn btn-info orange-tooltip edit-single-library" href="javascript:void(0)" title="Preview" style=" text-align: center;" ',
        'data-toggle="tooltip" title="Preview"  data-placement="bottom">',
        '<i class="glyphicon glyphicon-edit"></i>',
        '</button>'
    ].join('');
}

(function () {
    var singleLibraryController = function ($scope, $interval, $http, $location, $state, $rootScope, $window, $cookies, usSpinnerService, Idle, Keepalive, $uibModal, documentsHttpService) {

        var data = [];
        var url = $$ApiUrl + "/documents";

        function actionFormatterWeight(value, row, index) {
            return [
                '<button class="btn btn-info orange-tooltip edit-weight" href="javascript:void(0)" title="Редактировать" style=" text-align: center;" ',
                'data-toggle="tooltip" title="Редактировать вес"  data-placement="bottom">',
                '<i class="glyphicon glyphicon-edit"></i>',
                '</button>'
            ].join('');
        }

        $window.actionEventsSingleLibrary = {
            'click .edit-single-library': function (e, value, row, index) {
                BootstrapDialog.info({
                    title: 'Warning',
                    message: 'Function is not implemented yet!',
                    type: BootstrapDialog.TYPE_WARNING
                });
            }
        };


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