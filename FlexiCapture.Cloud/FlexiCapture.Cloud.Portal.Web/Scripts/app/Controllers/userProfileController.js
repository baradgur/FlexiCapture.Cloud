(function () {
    var userProfileController = function ($scope, $http, $location, $state, $rootScope, $window, $cookies, usSpinnerService, Idle, Keepalive, $uibModal,usersHttpService) {
           var url = $$ApiUrl + "/users";

        
        var userProfile = function () {
           
            $scope.loadData = false;
            usersHttpService.getToUserProfile($http, $scope, $state, url, usSpinnerService);
        };
        userProfile();

        $scope.updateUserProfile = function() {
            if ($scope.userProfileForm.$invalid ||
                    $scope.userProfileForm.password_confirmation.$viewValue != $scope.userProfileForm.password.$viewValue) {
                $scope.submitted = true;
                return;
            } else {
                $scope.submitted = false;
                $scope.currentUser.UserName = $scope.currentUser.Email;
                $scope.currentUser.Password = $scope.currentUser.NewPassword;
            }
            var anotherUrl = $$ApiUrl + "/manageuserprofile";
            usersHttpService.updateUserProfile($http, $scope, anotherUrl, usSpinnerService);
        }

    };


    fccApp.controller("userProfileController", ["$scope", "$http", "$location", "$state", "$rootScope", "$window", "$cookies", "usSpinnerService", "Idle", "Keepalive", "$uibModal","usersHttpService", userProfileController]);
}())