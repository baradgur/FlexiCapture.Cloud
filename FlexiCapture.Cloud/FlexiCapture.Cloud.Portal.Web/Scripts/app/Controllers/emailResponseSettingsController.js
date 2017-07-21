
fccApp.controller("emailResponseSettingsController", ["$scope", "$http", "$location", "$state", "$rootScope", "$window", "$cookies", "usSpinnerService", "Idle", "Keepalive", "$uibModal", "emailSettingsHttpService", "$animate", function ($scope,
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
       emailSettingsHttpService,
       $animate) {
    var url = $$ApiUrl + "/EmailResponseSettings";
    var data = [];

    $scope.userData = JSON.parse($window.sessionStorage.getItem("UserData"));

    $scope.responseSetting = {
        Id: 0,
        UserId: $scope.userData.UserData.Id,
        ShowJob: true,
        SendReply: true,
        AddAttachment: true,
        AddLink: true,
        CCResponse: false,
        Addresses: ""
    }

    $scope.getResponseSetting = function () {
        emailSettingsHttpService.getResponseEmailSettings($http, $scope, $state, data, url, usSpinnerService);
    }
    $scope.getResponseSetting();

    $scope.updateSetting = function() {
        emailSettingsHttpService.addResponseEmailSettings($http, $scope, $state, data, url, usSpinnerService);
    }

}]);
