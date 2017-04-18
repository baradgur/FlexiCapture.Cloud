(function () {
    var userRestoreController = function ($scope, $http, $location, $state, $rootScope, $window, $cookies, usSpinnerService, Idle, Keepalive, $uibModal) {
        $scope.userRestoreEmail = "";
        var url = $$ApiUrl + "/restoreData";
        var userRestore = function () {

            $scope.loadData = false;

        };
        userRestore();

        $scope.restorePassword = function () {

            usSpinnerService.spin('spinner-1');

            $scope.loadData = true;

            $http.get(url, {
                params: { email:$scope.userRestoreEmail }
            })
                .then(function (response) {
                    usSpinnerService.stop('spinner-1');
                    $scope.loadData = false;
                    var answer = response.data;
                    //alert(answer);
                    if (answer != "OK") {
                    answer = JSON.parse(answer);
                        
                        //  BootstrapDialog.show({
                        var dialog = new BootstrapDialog({
                            type: BootstrapDialog.TYPE_DANGER,
                            size: BootstrapDialog.SIZE_SMALL,
                            title: answer.Name,
                            message: "<div style='text-align:center'>" + answer.ShortDescription + "</br>" + answer.FullDescription + "</div>"
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

                        //   

                    }

                    // success
                },
                function (response) { // optional
                    // failed
                });
        }
    };



    fccApp.controller("userRestoreController", ["$scope", "$http", "$location", "$state", "$rootScope", "$window", "$cookies", "usSpinnerService", "Idle", "Keepalive", "$uibModal", userRestoreController]);
}())