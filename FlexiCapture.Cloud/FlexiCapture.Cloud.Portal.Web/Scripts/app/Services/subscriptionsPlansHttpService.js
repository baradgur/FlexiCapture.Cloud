fccApp.service('subscriptionsPlansHttpService', function () {

    //get to plans list
    this.getToPlans = function ($http, $scope, url, usSpinnerService) {
        $scope.plans = [];
        $scope.types = [];
        $scope.states = [];

        usSpinnerService.spin("spinner-1");

        $http({
            url: url,
            type: 'GET'
        })
            .then(function (response) {
                //$scope.userData.UserData
                if (response.data != null) {
                    $scope.plans = response.data.Plans ? response.data.Plans : [];
                    $scope.types = response.data.Types ? response.data.Types : [];
                    $scope.states = response.data.States ? response.data.States : [];
                } else {
                    showNotify("Fail", "Failed to load data", "danger");
                }

                $('#table').bootstrapTable({
                    data: $scope.plans,
                    height: '100%',
                    onPostBody: function () {
                        $('#table').bootstrapTable('resetView');
                    }

                });

                $('[data-toggle="tooltip"]').tooltip();

                $('#table').bootstrapTable('resetWidth');


                $scope.loading = false;
                usSpinnerService.stop('spinner-1');
            },
            function (response) {
                showNotify("Fail", "Failed to load data", "danger");
                $('#table').bootstrapTable({
                    data: $scope.plans,
                    height: '100%',
                    onPostBody: function () {
                        $('#table').bootstrapTable('resetView');
                    }

                });

                $('[data-toggle="tooltip"]').tooltip();

                $('#table').bootstrapTable('resetWidth');


                $scope.loading = false;
                usSpinnerService.stop('spinner-1');
            });
    }

    //get to user plan for current userSubscriptionController
    this.getToPlan = function ($http, $scope, url, usSpinnerService) {

        usSpinnerService.spin("spinner-1");
        
        $http({
            url: url+"/"+$scope.userData.UserData.Id,
            type: 'GET'
        })
            .then(function (response) {
                //$scope.userData.UserData
                if (response.data != null) {
                    $scope.plans = response.data.Plans ? response.data.Plans : [];
                    $scope.plan1s = [];
                    $scope.plan2s = [];
                    $scope.plan3s = [];
                    for (var i = 0; i < $scope.plans.length; i++) {
                        switch($scope.plans[i].SubscriptionPlanTypeId) {
                            case 1:
                                $scope.plan1s.push($scope.plans[i]);
                                break;
                            case 2:
                                $scope.plan2s.push($scope.plans[i]);
                                break;
                            case 3:
                                $scope.plan3s.push($scope.plans[i]);
                                break;
                        }
                    }

                    $scope.currentPlan = response.data.PlanUses ? response.data.PlanUses[0] : null;
                    if ($scope.currentPlan) {
                        $scope.currentPlan.endPlanPeriod = $scope.currentPlan.NextSubscriptionPlanId ? 2 : 1;
                        $scope.compareNextPlanId = $scope.currentPlan.NextSubscriptionPlanId;
                    }

                } else {
                    showNotify("Fail", "Failed to load data", "danger");
                }
                $scope.loading = false;
                usSpinnerService.stop('spinner-1');
            },
            function (response) {
                showNotify("Fail", "Failed to load data", "danger");
                $scope.loading = false;
                usSpinnerService.stop('spinner-1');
            });
    }

    // delete position
    this.deleteSelectedPositions = function ($http, $scope, data, deleteData, url, usSpinnerService) {
        usSpinnerService.spin("spinner-1");
        var methodType = "DELETE";
        $http({
            url: url,
            method: methodType,
            headers: {
                'Content-Type': 'application/json'
            },
            data: JSON.stringify(deleteData)
        })
            .then(function (response) {
                $scope.loadData = false;
                if (response.data == "Success") {

                    data = [];

                    for (var i = 0; i < deleteData.length; i++) {
                        for (var j = 0; j < $scope.documents.length; j++) {
                            if (deleteData[i].Id == $scope.documents[j].Id) {
                                $scope.documents.splice(j, 1);
                            }
                        }
                    }

                    for (var i = 0; i < $scope.documents.length; i++) {
                        var dElement = addData($scope.documents[i]);
                        data.push(dElement);
                    }

                    $('#table').bootstrapTable('load', data);
                    showNotify("Success", "Documents have been deleted successfully!", "success");
                    // success
                } else {
                    showNotify("Fail", "Failed to delete documents", "danger");
                }
                usSpinnerService.stop('spinner-1');
                $scope.loading = false;
                $('#table').bootstrapTable('resetWidth');
            },
                function (response) { // optional
                    // failed
                    $scope.loading = false;
                    usSpinnerService.stop('spinner-1');
                    showNotify("Fail", "Failed to delete documents", "danger");
                    $('#table').bootstrapTable('resetWidth');
                });
        $('#table').bootstrapTable('resetWidth');
    }

    //add or Edit plan
    this.managePlan = function ($http, $scope, url, usSpinnerService) {

        usSpinnerService.spin("spinner-1");
        var methodType = "POST";
        if ($scope.isEdit) {
            methodType = "PUT";
        }

        $http({
            url: url,
            method: methodType,
            contentType: "application/json",
            data: JSON.stringify($scope.plan)
        })
            .then(function (response) {

                usSpinnerService.stop('spinner-1');
                $scope.loadData = false;

                if (!response.data) {
                    showNotify("Danger", "Error occurred while " + +($scope.isEdit ? "updating" : "adding") + " plan", "danger");
                } else {
                    if (!$scope.isEdit) {
                        $scope.plans.push(response.data);
                    } else {
                        for (var j = 0; j < $scope.plans.length; j++) {
                            if (response.data.Id == $scope.plans[j].Id) {
                                $scope.plans[j] = response.data;
                                break;
                            }
                        }
                    }
                    showNotify("Danger", "Plan was successfully " + ($scope.isEdit ? "updated" : "added") + "!", "success");
                }
                $('#table').bootstrapTable('load', $scope.plans);
                $('#table').bootstrapTable('resetWidth');
                // success

            },
                function (response) { // optional
                    // failed
                    usSpinnerService.stop('spinner-1');
                    showNotify("Успех", "Error occurred while " + +($scope.isEdit ? "updating" : "adding") + " plan", "danger");
                    $('#table').bootstrapTable('resetWidth');
                });
        $('#table').bootstrapTable('resetWidth');

    }

    //add or Edit plan
    this.managePlanUse = function ($http, $scope, url, usSpinnerService) {

        usSpinnerService.spin("spinner-1");
        var methodType = "POST";
        if ($scope.isEdit) {
            methodType = "PUT";
        }

        $http({
            url: url,
            method: methodType,
            contentType: "application/json",
            data: JSON.stringify($scope.planUse)
        })
            .then(function (response) {

                usSpinnerService.stop('spinner-1');

                if (!response.data) {
                    showNotify("Danger", "Error occurred while " + +($scope.isEdit ? "updating" : "adding") + " pack", "danger");
                } else {
                    if (!$scope.isEdit) {
                        if (response.data.SubscriptionPlanUseStateId == 2) {
                            $scope.currentPlan = response.data;
                        }
                    } else {
                        $scope.currentPlan = response.data;
                    }
                    $scope.currentPlan.endPlanPeriod = $scope.currentPlan.NextSubscriptionPlanId ? 2 : 1;
                    $scope.compareNextPlanId = $scope.currentPlan.NextSubscriptionPlanId;
                    showNotify("Success", "Pack was successfully " + ($scope.isEdit ? "updated" : "added") + "!", "success");
                }
            },
                function(response) { // optional
                    // failed
                    usSpinnerService.stop('spinner-1');
                    showNotify("Успех",
                        "Error occurred while " + +($scope.isEdit ? "updating" : "adding") + " data",
                        "danger");
                });
    }

    // check if user's card info is already in vault
    this.checkPaypal = function ($http, $scope, url, usSpinnerService) {
        var dfd = jQuery.Deferred();

        usSpinnerService.spin("spinner-1");

        $http({
            url: url + "?userId=" + $scope.userData.UserData.Id,
            method: "GET"
            //contentType: "application/json",
            //data: { userId: JSON.stringify($scope.userData.UserData.Id) }
            })
            .then(function (response) {
                return dfd.resolve(response.data);
            }, function(response) {
                return dfd.resolve(null);
            });

        return dfd.promise();
    }

    this.sendInfoToVault = function ($http, $scope, url, usSpinnerService) {
        var dfd = jQuery.Deferred();

        usSpinnerService.spin("spinner-1");

        $http({
            url: url,
            method: "POST",
            contentType: "application/json",
            headers: {
                'Authorization': "Basic QVZpdGdIT0hfclgwSnBrRm5BdUljRTNYVlJRU0laZU1rSHJzZGNPRnU1RUIzS0NtRWlVczYxNlZ1emFuUWY1NmFjN052amtqTEt4NWFEbi06RUtyc2MtQUI2OFpueENSVXNMWk42Rl9ZVzRHQUdWZkhMek9vRkVURUJtYzJCM0JQdW1sMWZqeEtTU2xEY3ZnaXFGVnkyNV9OZThkVDhMSGY="
                    //"Bearer " + "A21AAG0P7Pim83FX_OwNT9Ez5GAUM59LjC_mn4dVmqp93NjU38gRaM5qJoPDg-VVyCw-UYzYoWWbOPmFVvuQPVXuOih4uYAYw"
            },
            data: JSON.stringify($scope.cardInfo)
        })
            .then(function (response) {

                return dfd.resolve(response.data);
            });

        return dfd.promise();
    }

    this.putVaultInfoToDb = function ($http, $scope, url, usSpinnerService, pairToPut) {
        var dfd = jQuery.Deferred();

        usSpinnerService.spin("spinner-1");

        $http({
            url: url,
            method: "POST",
            dataType: "application/json",
            data: JSON.stringify(pairToPut)
        }).then(function(response) {
            return dfd.resolve(response);
        });

        return dfd.promise();
    }

    this.sendPayment = function ($http, $scope, url, usSpinnerService, payment) {
        var dfd = jQuery.Deferred();

        usSpinnerService.spin("spinner-1");

        $http({
            url: url,
            method: "POST",
            dataType: "application/json",
            data: JSON.stringify(payment)
        }).then(function(repsonse) {
            return dfd.resolve(response.data);
        });

        return dfd.promise();
    }
});