(function () {
    var userSubscriptionsController = function($scope,
        $http,
        $location,
        $state,
        $rootScope,
        $window,
        $cookies,
        usSpinnerService,
        Idle,
        Keepalive,
        $uibModal) {

        $scope.monthToYearSwitcher = false;

        $scope.oneTimePurchaseBlocks = [
            {
                pagesCount: 50,
                termNum: 3,
                term: "months",
                cost: 0
            },
            {
                pagesCount: 500,
                termNum: 1,
                term: "year",
                cost: 10
            },
            {
                pagesCount: 500,
                termNum: 1,
                term: "month",
                cost: 50
            },
            {
                pagesCount: 5000,
                termNum: 1,
                term: "year",
                cost: 100
            },
            {
                pagesCount: 5000,
                termNum: 1,
                term: "month",
                cost: 50
            }
        ];

        $scope.monthlySubscriptionBlocks = [
            {
                pagesCount: 500,
                term: "month",
                cost: 5
            },
            {
                pagesCount: 500,
                termNum: 1,
                term: "year",
                cost: 50
            }
        ];

    };

    fccApp.controller("userSubscriptionsController", ["$scope", "$http", "$location", "$state", "$rootScope", "$window", "$cookies", "usSpinnerService", "Idle", "Keepalive", "$uibModal", userSubscriptionsController]);
}())