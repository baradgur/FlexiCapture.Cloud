(function () {
    var notificationsPreferencesController = function($scope,
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

        $scope.userData = JSON.parse($window.sessionStorage.getItem("UserData"));

        $scope.portalUpdatedNotif = false;
        $scope.importantNotif = true;
        $scope.monthlyUse = false;
    }


    fccApp.controller("notificationsPreferencesController", ["$scope", "$http", "$location", "$state", "$rootScope", "$window", "$cookies", "usSpinnerService", "Idle", "Keepalive", "$uibModal", notificationsPreferencesController]);
}())