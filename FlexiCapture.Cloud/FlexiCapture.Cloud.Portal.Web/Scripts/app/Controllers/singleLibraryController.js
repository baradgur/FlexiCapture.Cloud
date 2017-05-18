function actionFormatterSingleLibrary(value, row, index) {
    return [
        '<button class="btn btn-info orange-tooltip edit-single-library" href="javascript:void(0)" title="Preview" style=" text-align: center;" ',
        'data-toggle="tooltip" title="Preview"  data-placement="bottom">',
        '<i class="glyphicon glyphicon-edit"></i>',
        '</button>'
    ].join('');
}

function deleteFormatterFileSingleLibrary(value, row, index) {
    return [
        '<button class="btn btn-danger orange-tooltip delete-single-library" href="javascript:void(0)" title="Delete" style=" text-align: center;" ',
        'data-toggle="tooltip" title="Delete"  data-placement="bottom">',
        '<i class="glyphicon glyphicon-remove"></i>',
        '</button>'
    ].join('');
}


(function () {
    var singleLibraryController = function ($scope, $interval, $http, $location, $state, $rootScope, $window, $cookies, usSpinnerService, Idle, Keepalive, $uibModal, documentsHttpService) {

        var data = [];
        var url = $$ApiUrl + "/documents";


        $window.actionEventsSingleLibrary = {
            'click .edit-single-library': function (e, value, row, index) {
                BootstrapDialog.show({
                    title: 'Warning',
                    message: 'Function is not implemented yet!',
                    type: BootstrapDialog.TYPE_WARNING
                });
            },
            'click .delete-single-library': function (e, value, row, index) {
                BootstrapDialog.show({
                    title: 'Delete file',
                    message: 'Are you shure?',
                    buttons: [{
                        label: 'Yes',
                        action: function (dialog) {
                            documentsHttpService.deleteSelectedPositions($http, $scope, data,
                                [{
                                    'Id': row.Id,
                                    'TaskId': row.taskId
                                }],
                                url, usSpinnerService);
                            dialog.close();
                        }
                    }, {
                        label: 'Cancel',
                        action: function (dialog) {
                            dialog.close();
                        }
                    }]
                });
                
            }
        };


        var timer;
        if (!timer) {
            timer = $interval(function () {
                //console.log('Start silence!');
                documentsHttpService.getToDocumentsSilent($http, $scope, $state, data, url, usSpinnerService);
            }
                , 500000);
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
                BootstrapDialog.show({
                    title: 'Delete file',
                    message: 'Are you shure?',
                    buttons: [{
                        label: 'Yes',
                        action: function (dialog) {
                            documentsHttpService.deleteSelectedPositions($http, $scope, data, deleteData, url, usSpinnerService);
                            dialog.close();
                        }
                    }, {
                        label: 'Cancel',
                        action: function (dialog) {
                            deleteData = [];
                            dialog.close();
                        }
                    }]
                });
                
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