(function () {
    var userRegistrationController = function ($scope, $http, $location, $state, $rootScope, $window, $cookies, usSpinnerService, Idle, Keepalive, $uibModal) {
        var url = $$ApiUrl + "/userRegistration";
        $scope.newUser = {};
        $scope.rResponse = "";
        var userRegistration = function () {

            $scope.loadData = false;

        };
        userRegistration();

        //make registration
        $scope.checkRecaptcha = function () {
            $http({
                url: "https://www.google.com/recaptcha/api/siteverify",
                method: "POST",
                data: { secret: "6LcbtB0UAAAAAMGSWHdQAI7hs7hCZOf76fFsJA-N", response: $scope.rResponse }
            })
                .then(function (response) {


                    console.log(response);

                    // success
                },
                function (response) { // optional
                    // failed
                });

        }
        //make registration
        $scope.registerUser = function () {

            $scope.rResponse = window.grecaptcha.getResponse();
            if ($scope.registrationForm.$invalid ||
                $scope.registrationForm.password_confirmation.$viewValue != $scope.registrationForm.password.$viewValue) {
                $scope.submitted = true;
                return;
            } else {
                $scope.submitted = false;
            }

            if ($scope.rResponse == "") {
                $scope.submitted = true;
                var dialog = new BootstrapDialog({
                    type: BootstrapDialog.TYPE_DANGER,
                    size: BootstrapDialog.SIZE_SMALL,
                    title: "Captcha not selected",
                    message: "<div style='text-align:center'>Please click to the captcha</div>"
                });
                dialog.setSize(BootstrapDialog.SIZE_SMALL);
                dialog.open();
                return;
            }
            //alert(JSON.stringify($scope.rResponse));
            //$scope.checkRecaptcha();
            usSpinnerService.spin('spinner-1');

            $scope.loadData = true;
            $scope.newUser.CaptchaResponse = $scope.rResponse;
            $scope.newUser.UserName = $scope.newUser.Email;
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
                            message: "Congratulations, "+authModel.FirstName+" "+authModel.LastName+"! You've been succesfully registered!",
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