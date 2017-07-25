(function () {
    var dashBoardController = function ($scope, $http, $location, $state, $rootScope, $window, $cookies, usSpinnerService, Idle, Keepalive, $uibModal) {

        $scope.profileShowing = false;
        var dash = function () {
            $scope.serviceStateId = 1;
            $scope.userData = JSON.parse($window.sessionStorage.getItem("UserData"));
            if ($window.sessionStorage.serviceStateId !== undefined) {
                $scope.serviceStateId = parseInt($window.sessionStorage.serviceStateId);
            }

            switch ($scope.userData.UserData.UserRoleId) {
                case 1:
                    if ($window.sessionStorage.lastState != null && $window.sessionStorage.lastState != "main.dashboard") {
                        $state.go($window.sessionStorage.lastState);
                    } else {
                        $state.go("main.dashboard.users");
                    }
                    $scope.serviceStateId = -1;
                    break;

                case 2:
                    if ($window.sessionStorage.lastState != null && $window.sessionStorage.lastState != "main.dashboard") {
                        $state.go($window.sessionStorage.lastState);
                    } else {
                        $state.go("main.dashboard.single");
                    }
                    break;
                case 3:
                    if ($window.sessionStorage.lastState != null && $window.sessionStorage.lastState != "main.dashboard") {
                        $state.go($window.sessionStorage.lastState);
                    } else {
                        $state.go("main.dashboard.single");
                    }
                    break;
                case 4:
                    if ($window.sessionStorage.lastState != null && $window.sessionStorage.lastState != "main.dashboard") {
                        $state.go($window.sessionStorage.lastState);
                    } else {
                        $state.go("main.dashboard.library");
                    }
                    break;

                default:
                    $state.go("main.login");
                    break;
            }
            $scope.loadData = false;

        };
        dash();

        $scope.exitQuestion = function () {
            var dialog = BootstrapDialog.confirm({
                title: 'Warning',
                message: 'Do you really want to quit?',
                type: BootstrapDialog.TYPE_WARNING,
                size: BootstrapDialog.SIZE_SMALL,
                closable: true,
                btnCancelLabel: 'No',
                btnOKLabel: 'OK',
                btnOKClass: 'btn-warning',
                callback: function (result) {
                    if (result) {
                        $scope.exitApp();
                    }
                }
            });
            dialog.setSize(BootstrapDialog.SIZE_SMALL);
        };

        $scope.exitApp = function () {

            for (var key in $rootScope.sockets) {
                $rootScope.sockets[key].emit('dis');
                $rootScope.sockets[key].disconnect(true);
            }

            $rootScope.sockets = undefined;
            $rootScope.socketsActive = false;
            $cookies.remove("UserData", { path: "/" });
            $window.sessionStorage.clear();
            $state.go("main.login");
        }
        /// add a session timeout
        $scope.started = false;

        function closeModals() {
            if ($scope.warning) {
                $scope.warning.close();
                $scope.warning = null;
            }
        }

        $scope.$on('IdleStart', function () {
            closeModals();
            $scope.warning = $uibModal.open({
                templateUrl: 'warning-dialog.html',
                windowClass: 'modal-danger'
            });
        });

        $scope.$on('IdleEnd', function () {
            closeModals();
        });

        $scope.$on('IdleTimeout', function () {
            closeModals();
            $scope.exitApp();
        });

        $scope.start = function () {
            closeModals();
            Idle.watch();
            $scope.started = true;
        };
        $scope.start();

        $scope.navigate = function(navId) {
            switch (navId) {
                case 1:
                    $state.go("main.dashboard.subscr");
                    break;
            }
        }

        //navigate
        $scope.selectService = function (serviceId, anchor, isAvailable) {
            if (serviceId == $scope.serviceStateId) return;
            if (!isAvailable && serviceId !== 5  && serviceId!== 6) {
                serviceId = 6;
            }
            var previousServiceStateId = angular.copy($scope.serviceStateId);
            $window.sessionStorage.serviceStateId = serviceId;
            $scope.serviceStateId = serviceId;
            switch (serviceId) {
                case 1:
                    $state.go("main.dashboard.single");
                    break;

                case 2:
                    $state.go("main.dashboard.batch");
                    break;

                case 3:
                    $state.go("main.dashboard.ftpsettings");
                    break;

                case 4:
                    $state.go("main.dashboard.emailsettings");
                    break;

                case 5:
                    $state.go("main.dashboard.profile");
                    break;

                case 6:
                    if ($scope.userData.UserData.UserRoleId == 1 || $scope.userData.UserData.UserRoleId == 2) {
                        $state.go("main.dashboard.store", { '#': anchor });
                    } else {
                        $window.sessionStorage.serviceStateId = previousServiceStateId;
                        $scope.serviceStateId = previousServiceStateId;
                    }
                    break;
                    

            }
        };

    };


    fccApp.controller("dashBoardController", ["$scope", "$http", "$location", "$state", "$rootScope", "$window", "$cookies", "usSpinnerService", "Idle", "Keepalive", "$uibModal", dashBoardController]);
}())