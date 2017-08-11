function actionFormatterPlan(value, row, index) {
    return [
        '<button class="btn btn-info orange-tooltip edit-plan" href="javascript:void(0)" title="Edit Plan" style=" text-align: center;" ',
        'data-toggle="tooltip" title="Edit Plan"  data-placement="bottom">',
        '<i class="glyphicon glyphicon-edit"></i>',
        '</button>'
    ].join('');
}

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

        var url = $$ApiUrl + "/SubscriptionPlans";

        $scope.loadPlans = function () {
            $scope.loading = true;
            usSpinnerService.spin("spinner-1");
            subscriptionsPlansHttpService.getToPlans($http, $scope, url, usSpinnerService);
        }
        $scope.loadPlans();

        $window.actionEventsPlan = {
            'click .edit-plan': function (e, value, row, index) {
                $scope.addNewPlan(row);
            }
        };

        //$("#delete-plan").on('click',
        //    function() {
        //        var positionsToDelete = $('#table').bootstrapTable('getSelections');
        //        if (positionsToDelete.length > 0) {
        //            var deleteData = [];
        //            for (var i = 0; i < positionsToDelete.length; i++) {
        //                deleteData.push(positionsToDelete[i].id);
        //            };
        //            BootstrapDialog.show({
        //                title: 'Delete plan',
        //                message: 'Are you sure?',
        //                buttons: [
        //                    {
        //                        label: 'Yes',
        //                        action: function(dialog) {
        //                            $('#table').bootstrapTable('remove',
        //                            {
        //                                field: 'id',
        //                                values: deleteData
        //                            });
        //                            //documentsHttpService.deleteSelectedPositions($http, $scope, data, deleteData, url, usSpinnerService);
        //                            dialog.close();
        //                        }
        //                    }, {
        //                        label: 'Cancel',
        //                        action: function(dialog) {
        //                            deleteData = [];
        //                            dialog.close();
        //                        }
        //                    }
        //                ]
        //            });

        //        } else {
        //            BootstrapDialog.alert({
        //                title: 'Warning',
        //                message: 'There were no documents selected to delete!',
        //                type: BootstrapDialog.TYPE_WARNING
        //            });
        //        }
        //    });

        $scope.addNewPlan = function (plan) {

            if (plan) {
                $scope.isEdit = true;
            } else {
                $scope.isEdit = false;
                plan = {
                    SubscriptionPlanStateId:1,
                    SubscriptionPlanTypeId: 1
                };
            }

            $scope.plan = plan;

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
                subscriptionsPlansHttpService.managePlan($http, $scope, url, usSpinnerService);
                },
                function() {
                    console.log('Modal dismissed at: ' + new Date());
                });
        };


    }]);
