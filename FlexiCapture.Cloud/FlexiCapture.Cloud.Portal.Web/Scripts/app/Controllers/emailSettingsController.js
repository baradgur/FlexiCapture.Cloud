function actionFormatterEmailSetting(value, row, index) {
    return [
        '<button class="btn btn-info orange-tooltip edit-user" href="javascript:void(0)" title="Edit" style=" text-align: center;" ',
        'data-toggle="tooltip" title="Edit User"  data-placement="bottom">',
        '<i class="glyphicon glyphicon-edit"></i>',
        '</button>'
    ].join('');
}

(function () {
    var emailSettingsController = function ($scope, $http, $location, $state, $rootScope, $window, $cookies, usSpinnerService, Idle, Keepalive, $uibModal, emailSettingsHttpService) {
        var url = $$ApiUrl + "/EmailSettings";
        var data = [];

        $scope.userData = JSON.parse($window.sessionStorage.getItem("UserData"));

        $window.actionEventsEmailSetting = {
            'click .edit-user': function (e, value, row, index) {
                $scope.updateSetting(row);
            }
        };

        var emailSettings = function () {

            $scope.loadData = false;
            emailSettingsHttpService.getToEmailSettingsList($http, $scope, $state, data, url, usSpinnerService);
        };
        emailSettings();

        $scope.updateSetting = function (row) {
            var singleSetting = {};
            var settingId = row.settingId;
            for (var i = 0; i < $scope.settings.length; i++) {
                if ($scope.settings[i].Id == settingId) {
                    singleSetting = angular.copy($scope.settings[i]);
                }
            }
            $scope.gotoAddNewSetting(singleSetting);
        }

        //add new user btn event
        $scope.gotoAddNewSetting = function (setting) {

            if (setting) {
                $scope.isEdit = true;
            }
            else {
                setting = {};
                $scope.isEdit = false;
            }

            $scope.setting = setting;

            var modalInstance = $uibModal.open({
                templateUrl: 'PartialViews/EmailSettingManagement.html',
                controller: emailSettingManageController,
                controllerAs: 'vm',
                scope: $scope,
                resolve: {
                    items: function () {
                        return $scope.items;
                    }
                }
            });

            modalInstance.result.then(function () {

                $scope.setting.UserId = $scope.choosedUserId;

                emailSettingsHttpService.manageSetting($http, $scope, data, url, usSpinnerService, $scope.isEdit);
            }, function () {

            });
        }

    };


    fccApp.controller("emailSettingsController", ["$scope", "$http", "$location", "$state", "$rootScope", "$window", "$cookies", "usSpinnerService", "Idle", "Keepalive", "$uibModal", "emailSettingsHttpService", emailSettingsController]);
}())