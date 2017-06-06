function actionFormatterBatchLibrary(value, row, index) {
    return [
        '<button class="btn btn-info orange-tooltip edit-batch-library" href="javascript:void(0)" title="Preview" style=" text-align: center;" ',
        'data-toggle="tooltip" title="Preview"  data-placement="bottom">',
        '<i class="glyphicon glyphicon-edit"></i>',
        '</button>'
    ].join('');
}

function deleteFormatterFileBatchLibrary(value, row, index) {
    return [
        '<button class="btn btn-danger orange-tooltip delete-batch-library" href="javascript:void(0)" title="Delete" style=" text-align: center;" ',
        'data-toggle="tooltip" title="Delete"  data-placement="bottom">',
        '<i class="glyphicon glyphicon-remove"></i>',
        '</button>'
    ].join('');
}

function downloadFormatterLibrary(value, row, index) {
    return [
        "<a class='download-link' href ='javascript: void(0)'><i class='fa fa-download' aria-hidden='true'></i> Original File(s)</a>"

    ].join('');
}

function resultFormatterLibrary(value, row, index) {
    return [
        '<button class="btn btn-info orange-tooltip result-link" href="javascript:void(0)" title="Results" style=" text-align: center;" ',
        'data-toggle="tooltip" title="Results"  data-placement="bottom">',
        '<i class="glyphicon glyphicon-download-alt"></i>',
        '</button>'
    ].join('');
}

(function () {
    var batchLibraryController = function ($scope, $http, $interval, $location, $state, $rootScope, $window, $cookies, $filter, usSpinnerService, Idle, Keepalive, $uibModal, documentsHttpService) {

var data = [];
        var url = $$ApiUrl + "/documents";

        $window.actionEventsBatchLibrary = {
            'click .edit-batch-library': function (e, value, row, index) {
                BootstrapDialog.show({
                    title: 'Warning',
                    message: 'Function is not implemented yet!',
                    type: BootstrapDialog.TYPE_WARNING
                });
            },
            'click .delete-batch-library': function (e, value, row, index) {
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
            },
            'click .download-link': function (e, value, row, index) {
                documentsHttpService.downloadDocumentById($http, $scope, row.Id, $$ApiUrl + "/downloadfile")
                    .then(function (data, status, headers) {
                        try {
                            headers = headers();

                            var filename = headers['x-file-name'];
                            var contentType = headers['content-type'];
                            var blob = new Blob([data], { type: contentType });

                            //Check if user is using IE
                            var ua = window.navigator.userAgent;
                            var msie = ua.indexOf("MSIE ");

                            if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./)) {
                                window.navigator.msSaveBlob(blob, filename);
                            } else // If another browser, return 0
                            {
                                //Create a url to the blob
                                var url = window.URL.createObjectURL(blob);
                                var linkElement = document.createElement('a');
                                linkElement.setAttribute('href', url);
                                linkElement.setAttribute("download", filename);

                                //Force a download
                                var clickEvent = new MouseEvent("click", {
                                    "view": window,
                                    "bubbles": true,
                                    "cancelable": false
                                });
                                linkElement.dispatchEvent(clickEvent);
                            }

                        } catch (ex) {
                            console.log(ex);
                        }
                    });
            },
            'click .result-link': function (e, value, row, index) {
                $scope.downloadResults = [];
                var found = $filter('filter')($scope.documents, { Id: row.Id }, true);
                if (found.length > 0) {
                    $scope.downloadResults = found[0].ResultDocuments;
                }
                if ($scope.downloadResults.length > 0) {
                    var modalInstance = $uibModal.open({
                        templateUrl: 'PartialViews/Modals/DownloadResults.html',
                        controller: downloadResultsController,
                        controllerAs: 'vm',
                        scope: $scope,
                        resolve: {
                            items: function () {
                                return $scope.items;
                            }
                        }
                    });

                    modalInstance.result.then(function () {

                    }, function () {
                        console.log('Modal dismissed at: ' + new Date());
                    });
                }
                else {
                    BootstrapDialog.show({
                        title: 'Download files',
                        message: 'There are no files to download',
                        buttons: [{
                            label: 'OK',
                            action: function (dialog) {
                                dialog.close();
                            }
                        }]
                    });
                }
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

        
        var batchLibrary = function () {
            documentsHttpService.getToDocuments($http, $scope, $state, data, url, usSpinnerService);
            $scope.loadData = false;

        };
        batchLibrary();

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


    fccApp.controller("batchLibraryController", ["$scope", "$http", "$interval", "$location", "$state", "$rootScope", "$window", "$cookies", "$filter", "usSpinnerService", "Idle", "Keepalive", "$uibModal", "documentsHttpService", batchLibraryController]);
}())