function actionFormatterKey(value, row, index) {
    if (row.keyState == "Active") {
        return [
        '<button class="btn btn-warning orange-tooltip edit-key" href="javascript:void(0)" title="Disable key" style=" text-align: center;" ',
        'data-toggle="tooltip" title="Disable key"  data-placement="bottom">',
        '<i class="glyphicon glyphicon-remove"></i>',
        '</button>'
        ].join('');
    }
    return [
        '<button class="btn btn-success orange-tooltip edit-key" href="javascript:void(0)" title="Activate key" style=" text-align: center;" ',
        'data-toggle="tooltip" title="Activate key"  data-placement="bottom">',
        '<i class="glyphicon glyphicon-ok"></i>',
        '</button>'
    ].join('');
}

function actionFormatterDeleteKey(value, row, index) {
    return [
        '<button class="btn btn-danger orange-tooltip delete-key" href="javascript:void(0)" title="Delete key" style=" text-align: center;" ',
        'data-toggle="tooltip" title="Delete key"  data-placement="bottom">',
        '<i class="glyphicon glyphicon-trash"></i>',
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
        },
        'click .delete-key': function (e, value, row, index) {
        $scope.deleteKey(row);
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

    $scope.deleteKey = function (row) {
        var rParams = { id: row.keyId };
        onlineWebOcrSettingsHttpService.deleteKey($http, $scope, usSpinnerService, url, rParams, data);
    };

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