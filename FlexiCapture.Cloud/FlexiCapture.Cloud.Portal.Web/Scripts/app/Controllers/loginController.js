(function () {
    var loginController = function ($scope, $http, $location, $state, $rootScope, $window, usSpinnerService, $cookies) {
        var url = $$ApiUrl + "/login";
        $scope.rememberMe = false;
        
        // $scope.emailFormat = /^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$/;
        $scope.loadData = false;
       // $scope.khingalFactory = khingalFactory;
        $scope.loginUser = function () {
            //alert('Work!');
                if ($scope.loginForm.$invalid) {
                    $scope.submitted = true;
                    return;
                }
                usSpinnerService.spin('spinner-1');

                $scope.loadData = true;
                $scope.model = {};
                $scope.model.Id = -1;
                $scope.model.UserId = -1;
                $scope.model.UserLogin = $scope.Login;
                $scope.model.UserPassword = $scope.Password;
                $scope.loadData = false;

              // $state.go("main.dashboard");

                //

                $http({
                    url: url,
                    method: "POST",
                    data: $scope.model
                })
                    .then(function (response) {
                        usSpinnerService.stop('spinner-1');
                        $scope.loadData = false;

                        var authModel = response.data;
                        //configureMapArea($scope, authModel);
                        //console.log($scope.authData);

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
                        } else if (authModel.LoginData.UserLoginStateId == 2) {
                            var dialog = new BootstrapDialog({
                                type: BootstrapDialog.TYPE_WARNING,
                                size: BootstrapDialog.SIZE_SMALL,
                                title: "Пользователь заблокирован",
                                message: "<div style='text-align:center'>Данный пользователь заблокирован. За более подробной информацией обратитесь к администратору.</div>"
                            });
                            dialog.setSize(BootstrapDialog.SIZE_SMALL);
                            dialog.open();
                        }
                        else {
                            if ($scope.rememberMe) {
                                var expireDate = new Date();
                                expireDate.setDate(expireDate.getDate() + 10);
                                $cookies.putObject("UserData", authModel, { expires: expireDate, path:"/" });
                                $cookies.putObject("AuthData", $scope.authData, { expires: expireDate, path: "/" });
                            }
                            $window.sessionStorage.setItem("UserData", JSON.stringify(authModel));
                            // $window.sessionStorage.setItem("AuthData", JSON.stringify($scope.authData));

                            //добавляем данные из настроек

                        //     for (var i = 0; i < authModel.SettingsData.length; i++) {
                        //        if (authModel.SettingsData[i].SettingName == "PhoneNumberMask"+authModel.CompanyData.Id) {
                        //            $scope.khingalFactory.phoneMask = authModel.SettingsData[i].SettingValue;
                        //            $window.sessionStorage.setItem("PhoneMask", JSON.stringify($scope.khingalFactory.phoneMask));
                        //            if ($scope.rememberMe) {
                        //                var expireDate = new Date();
                        //                expireDate.setDate(expireDate.getDate() + 10);
                        //                $cookies.putObject("PhoneMask", $scope.khingalFactory.phoneMask, { expires: expireDate, path: "/" });
                        //            }
                        //        }
                        //    }
                            $state.go("main.dashboard");

                        }

                        // success
                    },
                        function (response) { // optional
                            // failed
                        });


        };


        setTimeout(function () {
            $("#password")
                .keypress(function (e) {
                    if (e.charCode == 13) {
                        $scope.loginUser();
                    }
                });
        },
            250);
    };

    fccApp.controller("loginController", ["$scope", "$http", "$location", "$state", "$rootScope", "$window", "usSpinnerService", "$cookies",  loginController]);
}())