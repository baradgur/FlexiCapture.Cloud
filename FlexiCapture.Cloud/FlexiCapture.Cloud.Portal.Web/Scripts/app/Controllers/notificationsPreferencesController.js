(function () {
    var notificationsPreferencesController = function($scope,
        $http,
        $location,
        $state,
        $rootScope,
        $window,
        $cookies,
        usSpinnerService,
        Idle,
        Keepalive,
        $uibModal,
        usersHttpService) {
        var url = $$ApiUrl + "/users";
        $scope.userData = JSON.parse($window.sessionStorage.getItem("UserData"));


        var userProfile = function () {

            $scope.loadData = false;
            usersHttpService.getToUserProfile($http, $scope, $state, url, usSpinnerService);
        };
        userProfile();

        $scope.updateUserProfile = function () {
            
                $scope.submitted = false;
                $scope.currentUser.Password = null;
            
            var anotherUrl = $$ApiUrl + "/manageuserprofile";
            usersHttpService.updateUserProfile($http, $scope, anotherUrl, usSpinnerService);
        }

        $scope.importantNotif = true;
    }


    fccApp.controller("notificationsPreferencesController", ["$scope", "$http", "$location", "$state", "$rootScope", "$window", "$cookies", "usSpinnerService", "Idle", "Keepalive", "$uibModal", "usersHttpService", notificationsPreferencesController]);
}())