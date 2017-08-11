(function () {
    var userSubscriptionsController = function ($scope,
        $http,
        $location,
        $state,
        $rootScope,
        $window,
        $cookies,
        usSpinnerService,
        Idle,
        Keepalive,
        $uibModal,
        subscriptionsPlansHttpService) {

        $scope.plans = [];
        $scope.currentPlan = null;
        var url = $$ApiUrl + "/SubscriptionPlans";
        var planUseUrl = $$ApiUrl + "/SubscriptionPlanUses";
        $scope.monthToYearSwitcher = false;

        $scope.userData = JSON.parse($window.sessionStorage.getItem("UserData"));

        $scope.loadPlans = function () {

            $scope.loading = true;
            usSpinnerService.spin("spinner-1");
            subscriptionsPlansHttpService.getToPlan($http, $scope, url, usSpinnerService);
        }
        $scope.loadPlans();

        $scope.endPlanPeriodChosen = function (value) {
            if (value == 1) {
                $scope.currentPlan.NextSubscriptionPlanId = null;
            }
            else {
                $scope.currentPlan.NextSubscriptionPlanId = $scope.currentPlan.SubscriptionPlanId;
            }
        }

        $scope.managePlanUse = function (planId) {

            if (!planId) {
                $scope.isEdit = true;
            } else {
                $scope.isEdit = false;

            }

            $scope.planUse = $scope.isEdit ? angular.copy($scope.currentPlan) : {
                UserId: $scope.userData.UserData.Id,
                SubscriptionPlanId: planId
            };
            
            subscriptionsPlansHttpService.managePlanUse($http, $scope, planUseUrl, usSpinnerService);
        };
    };

    fccApp.controller("userSubscriptionsController", ["$scope", "$http", "$location", "$state", "$rootScope", "$window", "$cookies", "usSpinnerService", "Idle", "Keepalive", "$uibModal", "subscriptionsPlansHttpService", userSubscriptionsController]);
}())