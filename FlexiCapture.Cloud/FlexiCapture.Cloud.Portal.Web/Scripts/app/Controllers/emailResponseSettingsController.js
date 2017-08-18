
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

    $scope.updateSetting = function () {
        if ($scope.responseSetting.SendReply) {
            if (!($scope.responseSetting.AddAttachment || $scope.responseSetting.AddLink)) {
                $scope.mustChooseRespSetting = true;
                return;
            }
        }

        if (!$scope.responseSetting.SendReply) {
            $scope.responseSetting.AddAttachment = false;
            $scope.responseSetting.AddLink = false;
            $scope.responseSetting.CCResponse = false;
        }
        if (!$scope.responseSetting.CCResponse) {
            $scope.responseSetting.Addresses = "";
        }

        var emailRegex = /^[^@^;\s]+@[^@\s]+\.[^@^;\s]+$/g;

        var emailAddressesToValidate = $scope.responseSetting.Addresses.split(';');
        var ifPassed = false;

        for (var i in emailAddressesToValidate) {
            ifPassed = emailRegex.test(emailAddressesToValidate[i]);
        }

        if (($scope.responseSetting.CCResponse && $scope.responseSetting.Addresses == "")||!ifPassed) {
            showNotify("Ошибка", "You need at least one e-mail address!", "danger");
            return;
        }

        emailSettingsHttpService.addResponseEmailSettings($http, $scope, $state, data, url, usSpinnerService);
        $scope.mustChooseRespSetting = false;

    }

    $scope.compareResponseSettings = function () {
        return angular.equals($scope.responseSetting, $scope.responseSettingToCompare);
    };

}]);
