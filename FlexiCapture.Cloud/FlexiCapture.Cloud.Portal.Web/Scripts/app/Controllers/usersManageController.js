
var usersManageController = function ($scope, $http, $location, $state, $uibModal, $uibModalInstance, $stateParams, usSpinnerService, usersHttpService, items) {
    var url = $$ApiUrl + "/users";
    var vm = this;
    var roleId = null;
    var loginStateId = null;

    if ($scope.user.UserData) {
        $scope.user.LoginData.UserLogin = angular.copy($scope.user.UserData.Email);
        roleId = $scope.user.UserData.UserRoleId;
        loginStateId = $scope.user.LoginData.UserLoginStateId;
    } else {
        $scope.user.UserData = {};
        $scope.user.LoginData = {};
        $scope.user.UserRoleData = {};
        $scope.user.ServiceData = {
            SingleFileConversionService: true,
            FTPFileConversionService: false,
            EmailAttachmentFileConversionService: false,
            BatchFileConversionService: false
        };
    }


    $scope.userRoles = { availableOptions: [], selectedOption: {} };

    var roleSelectList = function (roleId) {
        for (var i = 0; i < $scope.availableRoles.length; i++) {
            if ($scope.userData.UserData.UserRoleId == 2 && i < 2)
                i = 2;
            var user = { id: $scope.availableRoles[i].Id, name: $scope.availableRoles[i].Name };
            $scope.userRoles.availableOptions.push(user);
        }
        if (roleId) {
            var selectedOption = { id: roleId, name: "-----" };
            $scope.userRoles.selectedOption = selectedOption;
        }
    };
    roleSelectList(roleId);

    var stateSelectList = function (loginStateId) {
        $scope.loginStates = { availableOptions: [], selectedOption: {} };
        for (var i = 0; i < $scope.availableLoginStates.length; i++) {

            var unit = { id: $scope.availableLoginStates[i].Id, name: $scope.availableLoginStates[i].Name };
            $scope.loginStates.availableOptions.push(unit);

        }
        if (loginStateId) {
            var selectedOption = { id: loginStateId, name: "-----" };
            $scope.loginStates.selectedOption = selectedOption;
        }
    };
    stateSelectList(loginStateId);

    function loadUserData() {
        vm.$scope = $scope;
        vm.data = items;
        vm.user = $scope.user;
        $scope.submitted = false;
        vm.confirm = function () {
            if ($scope.userForm.$valid && $scope.user.UserData.UserRoleId) {
                if ($scope.user.UserData.Id == undefined) $scope.isEdit = false;

                if (!$scope.isEdit) {
                    $scope.user.UserData.Id = -1;
                    $scope.user.LoginData.UserLoginStateId = 1; // loginStateIsActive
                    $scope.user.UserData.UserName = $scope.user.LoginData.UserLogin;
                    $scope.user.UserData.Email = $scope.user.UserData.UserName;

                    $scope.user.UserData.ParentUserId = $scope.userData.UserData.UserRoleId == 2 ? $scope.userData.UserData.Id : null;
                } else {
                    $scope.user.UserData.UserName = $scope.user.LoginData.UserLogin;
                    $scope.user.UserData.Email = $scope.user.UserData.UserName;
                }
                usersHttpService.manageUser(vm.callbackUserUpdate, $http, $scope, vm.data, url, usSpinnerService, $scope.isEdit);
            }
            else { $scope.submitted = true; }
        }
        vm.cancel = $uibModalInstance.dismiss;

        vm.roleChanged = function () {
            $scope.user.UserData.UserRoleId = $scope.userRoles.selectedOption.id;
        }



        vm.stateChanged = function () {
            $scope.user.LoginData.UserLoginStateId = $scope.loginStates.selectedOption.id;
        }

        vm.callbackUserUpdate = function (responseSuccess, responseNoError) {
            if (responseSuccess && responseNoError) {
                $uibModalInstance.close();
            };
        }
    }
    loadUserData();

};



fccApp.controller("usersManageController", ["$scope", "$http", "$location", "$state", "$uibModal", "$stateParams", "usSpinnerService", "usersHttpService", usersManageController]);
fccApp.filter('females', [function () {
    return function (object) {
        var array = [];
        angular.forEach(object, function (person) {
            if (person.gender == 'female')
                array.push(person);
        });
        return array;
    };
}]);