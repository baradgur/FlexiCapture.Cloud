function actionFormatterFTPSetting(value, row, index) {
    return [
        '<button class="btn btn-info orange-tooltip edit-user" href="javascript:void(0)" title="Edit" style=" text-align: center;" ',
        'data-toggle="tooltip" title="Edit User"  data-placement="bottom">',
        '<i class="glyphicon glyphicon-edit"></i>',
        '</button>'
    ].join('');
}

(function () {
    var ftpSettingsController = function ($scope, $http, $location, $state, $rootScope, $window, $cookies, usSpinnerService, Idle, Keepalive, $uibModal, ftpSettingsHttpService) {
        var url = $$ApiUrl + "/FTPSettings";
        var data = [];

        $scope.userData = JSON.parse($window.sessionStorage.getItem("UserData"));

        $window.actionEventsFTPSetting = {
            'click .edit-user': function (e, value, row, index) {
                $scope.updateSetting(row);
            }
        };

        var ftpSettings = function () {
           
            $scope.loadData = false;
            ftpSettingsHttpService.getToFTPSettingsList($http, $scope, $state, data, url, usSpinnerService);
        };
        ftpSettings();

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
                templateUrl: 'PartialViews/FTPSettingManagement.html',
                controller: ftpSettingManageController,
                controllerAs: 'vm',
                scope: $scope,
                resolve: {
                    items: function () {
                        return $scope.items;
                    }
                }
            });

            modalInstance.result.then(function () {
                
                $scope.setting.UserId = $scope.userData.UserData.Id;

                ftpSettingsHttpService.manageSetting($http, $scope, data, url, usSpinnerService, $scope.isEdit);
            }, function () {
                
            });
        }

    };


    fccApp.controller("ftpSettingsController", ["$scope", "$http", "$location", "$state", "$rootScope", "$window", "$cookies", "usSpinnerService", "Idle", "Keepalive", "$uibModal", "ftpSettingsHttpService", ftpSettingsController]);
}())