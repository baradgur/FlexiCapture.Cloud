var ftpSettingManageController = function ($scope, $http, $location, $state, $uibModal, $uibModalInstance, $stateParams, usSpinnerService, items) {
    var vm = this;


    setTimeout(function () {
        var v = document.getElementById("path-toggle");
        if (v != null)
            $('#path-toggle').tooltip();
            $('#host-toggle').tooltip();
        },
        500);

    function loadSettingData() {
        vm.$scope = $scope;
        vm.items = items;
        vm.user = $scope.user;
        $scope.submitted = false;
        vm.confirm = function (field) {
            if ($scope.settingForm.$valid) {
                $scope.submitted = false;
                $uibModalInstance.close();
            }
            else { $scope.submitted = true; }
        }
        
        vm.cancel = $uibModalInstance.dismiss;
    }
    loadSettingData();

};



fccApp.controller("ftpSettingManageController", ["$scope", "$http", "$location", "$state", "$uibModal", "$stateParams", "usSpinnerService", ftpSettingManageController]);
