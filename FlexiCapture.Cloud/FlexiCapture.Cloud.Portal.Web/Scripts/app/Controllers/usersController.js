function actionFormatterUser(value, row, index) {
    return [
        '<button class="btn btn-info orange-tooltip edit-user" href="javascript:void(0)" title="Edit" style=" text-align: center;" ',
        'data-toggle="tooltip" title="Edit User"  data-placement="bottom">',
        '<i class="glyphicon glyphicon-edit"></i>',
        '</button>'
    ].join('');
}

function actionDeleteUser(value, row, index) {
    return [
        '<button class="btn btn-danger orange-tooltip delete-user" href="javascript:void(0)" title="Delete" style=" text-align: center;" ',
        'data-toggle="tooltip" title="Delte User"  data-placement="bottom">',
        '<i class="glyphicon glyphicon-trash"></i>',
        '</button>'
    ].join('');
}

(function() {


    var usersController = function($scope, $http, $location, $state, $uibModal, $log, $window, $filter, usSpinnerService, usersHttpService) {

        var url = $$ApiUrl + "/users";
        var data = [];
        $scope.userData = JSON.parse($window.sessionStorage.getItem("UserData"));

        $scope.loading = true;
        var getToUsersList = function() {
            usersHttpService.getToUsersList($http, $scope, $state, data, url, usSpinnerService);
        };
        getToUsersList();

        $window.actionEventsUser = {
            'click .edit-user': function(e, value, row, index) {
                $scope.updateUser(row);
                $scope.choosedUserId = row.userId;
            },

            'click .delete-user': function(e, value, row, index) {

                var dialog = BootstrapDialog.confirm({
                    title: 'Warning',
                    message: 'Do you really want to delete the user??',
                    type: BootstrapDialog.TYPE_WARNING,
                    size: BootstrapDialog.SIZE_SMALL,
                    closable: true,
                    btnCancelLabel: 'No',
                    btnOKLabel: 'Yes',
                    btnOKClass: 'btn-warning',
                    callback: function(result) {
                        if (result) {
                            $scope.deleteUser(row.userId);

                        }
                    }
                });
                dialog.setSize(BootstrapDialog.SIZE_SMALL);

            }
        };
        $scope.updateUser = function(row) {
            var singleUser = {};
            var userId = row.userId;
            for (var i = 0; i < $scope.users.length; i++) {
                if ($scope.users[i].UserData.Id == userId) {
                    singleUser = angular.copy($scope.users[i]);
                }
            }
            $scope.gotoAddNewUserView(singleUser);
        }

        $scope.deleteUser = function(id) {
            var rParams = { id: id };
            url = $$ApiUrl + "/users"
            usersHttpService.deleteUser($http, $scope, usSpinnerService, url, rParams);
        };

        $scope.editFtpSettings = function() {
            var modalInstance = $uibModal.open({
                templateUrl: 'PartialViews/FTPSettings.html',
                controller: ftpSettingManageController,
                controllerAs: 'vm',
                size: 'lg',
                scope: $scope,
                resolve: {
                    items: function() {
                        return $scope.items;
                    }
                }
            });
        }

        $scope.editEmailSettings = function() {
            var modalInstance = $uibModal.open({
                templateUrl: 'PartialViews/EmailSettings.html',
                controller: emailSettingManageController,
                controllerAs: 'vm',
                size: 'lg',
                scope: $scope,
                resolve: {
                    items: function() {
                        return $scope.items;
                    }
                }
            });
        }

        //add new user btn event
        $scope.gotoAddNewUserView = function(user) {

            if (user) {
                $scope.isEdit = true;
            } else {
                user = {};
                $scope.isEdit = false;
            }

            $scope.user = user;

            var modalInstance = $uibModal.open({
                templateUrl: 'PartialViews/UserManagement.html',
                controller: usersManageController,
                controllerAs: 'vm',
                scope: $scope,
                resolve: {
                    items: function() {
                        return data;
                    }
                }
            });

            modalInstance.result.then(function() {
                $log.info(JSON.stringify($scope.user));

            }, function() {
                $log.info('Modal dismissed at: ' + new Date());
            });
        }
    };




    fccApp.controller("usersController", ["$scope", "$http", "$location", "$state", "$uibModal", "$log", "$window", "$filter", "usSpinnerService", "usersHttpService", usersController]);
}())