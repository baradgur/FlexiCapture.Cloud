function actionFormatterKey(value, row, index) {
    return [
        '<button class="btn btn-info orange-tooltip edit-key" href="javascript:void(0)" title="Change key state" style=" text-align: center;" ',
        'data-toggle="tooltip" title="Change key state"  data-placement="bottom">',
        '<i class="glyphicon glyphicon-edit"></i>',
        '</button>'
    ].join('');
}

var onlineWebOcrSettingsController = function ($scope, $http, $location, $state, $rootScope, $window, $cookies, usSpinnerService, Idle, Keepalive, $uibModal, onlineWebOcrSettingsHttpService) {

    $scope.userData = JSON.parse($window.sessionStorage.getItem("UserData"));
    var data = [];
    var url = $$ApiUrl + "/OcrApiKeys";
    $scope.apiKeys = [];

    $window.actionEventsKey = {
        'click .edit-key': function (e, value, row, index) {
            $scope.updateKey(row);
        }
    };

    $scope.updateKey = function (row) {
        for (var i = 0; i < $scope.apiKeys.length; i++) {
            if ($scope.apiKeys[i].Id == row.keyId) {
                $scope.apiKey = angular.copy($scope.apiKeys[i]);
                $scope.apiKey.IsActive = !$scope.apiKey.IsActive;
            }
        }
        onlineWebOcrSettingsHttpService.manageKey($http, $scope, data, url, usSpinnerService, true);
    }

    $scope.addNewKey = function () {
        $scope.apiKey = { UserId: $scope.userData.UserData.Id };
        onlineWebOcrSettingsHttpService.manageKey($http, $scope, data, url, usSpinnerService, false);
    }

    var onlineWebOcrSettings = function() {

        $scope.loadData = false;
        onlineWebOcrSettingsHttpService.getToOcrApiKeys($http, $scope, $state, url, data, usSpinnerService);
    };
    onlineWebOcrSettings();

};

fccApp.controller("onlineWebOcrSettingsController", ["$scope", "$http", "$location", "$state", "$rootScope", "$window", "$cookies", "usSpinnerService", "Idle", "Keepalive", "$uibModal", "onlineWebOcrSettingsHttpService", onlineWebOcrSettingsController]);