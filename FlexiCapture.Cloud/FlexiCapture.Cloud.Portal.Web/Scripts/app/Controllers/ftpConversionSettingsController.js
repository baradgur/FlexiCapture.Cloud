
fccApp.controller("ftpConversionSettingsController", ["$scope", "$http", "$location", "$state", "$rootScope", "$window", "$cookies", "usSpinnerService", "Idle", "Keepalive", "$uibModal", "ftpSettingsHttpService", "$animate", function ($scope,
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
       ftpSettingsHttpService,
       $animate) {
    var url = $$ApiUrl + "/FTPConversionSettings";
    var data = [];

    $scope.userData = JSON.parse($window.sessionStorage.getItem("UserData"));

    $scope.ftpConversionSetting = {
        Id: 0,
        AddProcessed: false,
        ReturnResults: false,
        MirrorInput: false,
        MoveProcessed: false,
        UserId: 0
}

    $scope.getFtpConversionSetting = function () {
        ftpSettingsHttpService.getFtpConversionSettings($http, $scope, $state, data, url, usSpinnerService);
    }
    $scope.getFtpConversionSetting();

    $scope.updateSetting = function () {
       
        ftpSettingsHttpService.addFtpConversionSettings($http, $scope, $state, data, url, usSpinnerService);
        $scope.mustChooseRespSetting = false;

    }

    $scope.compareFtpConversionSettings = function () {
        return angular.equals($scope.ftpCoversionSetting, $scope.ftpCoversionSettingToCompare);
    };

}]);
