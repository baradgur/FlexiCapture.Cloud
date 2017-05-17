(function () {
    var confirmEmailController = function ($scope, $http, $stateParams, $location, $state, $rootScope, $window, $cookies, usSpinnerService, Idle, Keepalive, $uibModal) {

        var url = $$ApiUrl + "/confirmationEmail";
        var confirmEmail = function () {
            usSpinnerService.spin("spinner-1");
            var param2 = $stateParams.guid;
            

            $http.get(url, {
                params: { guid: param2 }
            }).then(function (response) {
                var answer = JSON.stringify(response.data);
                //alert(answer);
                usSpinnerService.stop("spinner-1");
                if (response.data == 'OK') {
                    var dialog = new BootstrapDialog({
                        type: BootstrapDialog.TYPE_SUCCESS,
                        size: BootstrapDialog.SIZE_SMALL,
                        title: "Email Confirmation Done!",
                        message: "<div style='text-align:center'>Email confirmation was successful.  Please login with your credentials.</div>",
                        onhidden: function (dialogRef) {
                                $state.go("main.login");
                            }
                    });
                    dialog.setSize(BootstrapDialog.SIZE_SMALL);
                    dialog.open();
                } else {
                    var dialog = new BootstrapDialog({
                        type: BootstrapDialog.TYPE_DANGER,
                        size: BootstrapDialog.SIZE_SMALL,
                        title: "Error Confirmation!",
                        message: "<div style='text-align:center'>Error confirmation email!</div>",
                        onhidden: function (dialogRef) {
                                $state.go("main.login");
                            }
                    });
                    dialog.setSize(BootstrapDialog.SIZE_SMALL);
                    dialog.open();
                }
                //alert(JSON.stringify(response));
            });
            $scope.loadData = false;

        };
        confirmEmail();

    };


    fccApp.controller("confirmEmailController", ["$scope", "$http", "$stateParams", "$location", "$state", "$rootScope", "$window", "$cookies", "usSpinnerService", "Idle", "Keepalive", "$uibModal", confirmEmailController]);
}())