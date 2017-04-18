(function () {
    var userRegistrationController = function ($scope, $http, $location, $state, $rootScope, $window, $cookies, usSpinnerService, Idle, Keepalive, $uibModal) {
        var url = $$ApiUrl + "/userRegistration";
        $scope.newUser = {};

        var userRegistration = function () {

            $scope.loadData = false;

        };
        userRegistration();

        //make registration
        $scope.registerUser = function () {


            usSpinnerService.spin('spinner-1');

            $scope.loadData = true;

            $http({
                url: url,
                method: "POST",
                data: $scope.newUser
            })
                .then(function (response) {
                    usSpinnerService.stop('spinner-1');
                    $scope.loadData = false;

                    var authModel = JSON.parse(response.data);

                    if (authModel.Error != null) {
                        //  BootstrapDialog.show({
                        var dialog = new BootstrapDialog({
                            type: BootstrapDialog.TYPE_DANGER,
                            size: BootstrapDialog.SIZE_SMALL,
                            title: authModel.Error.Name,
                            message: "<div style='text-align:center'>" + authModel.Error.ShortDescription + "</br>" + authModel.Error.FullDescription + "</div>"
                        });
                        dialog.setSize(BootstrapDialog.SIZE_SMALL);
                        dialog.open();
                    }
                    else {


                        var dialog = new BootstrapDialog({
                            type: BootstrapDialog.TYPE_SUCCESS,
                            size: BootstrapDialog.SIZE_SMALL,
                            title: "Successfull!",
                            message: "New pasword was sent to your email " + $scope.userRestoreEmail,
                            onhidden: function (dialogRef) {
                                $state.go("main.login");
                            }
                        });
                        dialog.setSize(BootstrapDialog.SIZE_SMALL);
                        dialog.open();

                    }

                    // success
                },
                function (response) { // optional
                    // failed
                });

        }

    };



    fccApp.controller("userRegistrationController", ["$scope", "$http", "$location", "$state", "$rootScope", "$window", "$cookies", "usSpinnerService", "Idle", "Keepalive", "$uibModal", userRegistrationController]);
}())