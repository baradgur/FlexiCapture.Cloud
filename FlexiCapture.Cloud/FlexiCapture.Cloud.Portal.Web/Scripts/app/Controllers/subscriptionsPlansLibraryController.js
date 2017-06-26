

fccApp.controller("subscriptionsPlansLibraryController", ["$scope", "$interval", "$http", "$location", "$state", "$rootScope", "$window", "$cookies", "$filter", "usSpinnerService", "Idle", "Keepalive", "$uibModal", "subscriptionsPlansHttpService",
    function ($scope,
        $interval,
        $http,
        $location,
        $state,
        $rootScope,
        $window,
        $cookies,
        $filter,
        usSpinnerService,
        Idle,
        Keepalive,
        $uibModal,
        subscriptionsPlansHttpService) {

        $scope.isWorking = false;

        $scope.loadPlans = function() {
            subscriptionsPlansHttpService.getToPlans($http, $scope, $state);
        }
        $scope.loadPlans();

        $("#delete-plan").on('click',
            function() {
                var positionsToDelete = $('#table').bootstrapTable('getSelections');
                if (positionsToDelete.length > 0) {
                    var deleteData = [];
                    for (var i = 0; i < positionsToDelete.length; i++) {
                        deleteData.push(positionsToDelete[i].id);
                    };
                    BootstrapDialog.show({
                        title: 'Delete plan',
                        message: 'Are you sure?',
                        buttons: [
                            {
                                label: 'Yes',
                                action: function(dialog) {
                                    $('#table').bootstrapTable('remove',
                                    {
                                        field: 'id',
                                        values: deleteData
                                    });
                                    //documentsHttpService.deleteSelectedPositions($http, $scope, data, deleteData, url, usSpinnerService);
                                    dialog.close();
                                }
                            }, {
                                label: 'Cancel',
                                action: function(dialog) {
                                    deleteData = [];
                                    dialog.close();
                                }
                            }
                        ]
                    });

                } else {
                    BootstrapDialog.alert({
                        title: 'Warning',
                        message: 'There were no documents selected to delete!',
                        type: BootstrapDialog.TYPE_WARNING
                    });
                }
            });



        $("#add-plan").on('click', function() {
            var modalInstance = $uibModal.open({
                templateUrl: 'PartialViews/Modals/NewPlan.html',
                controller: addPlanController,
                controllerAs: 'vm',
                scope: $scope,
                resolve: {
                    items: function() {
                        return $scope.items;
                    }
                }
            });
            modalInstance.result.then(function() {

                },
                function() {
                    console.log('Modal dismissed at: ' + new Date());
                });
        });


    }]);
