(function () {
    var userProfileController = function ($scope, $http, $location, $state, $rootScope, $window, $cookies, usSpinnerService, Idle, Keepalive, $uibModal,usersHttpService) {
           var url = $$ApiUrl + "/users";

        
        var userProfile = function () {
           
            $scope.loadData = false;
            usersHttpService.getToUserProfile($http, $scope, $state, url, usSpinnerService);

            //alert("E");
        };
        userProfile();

    };


    fccApp.controller("userProfileController", ["$scope", "$http", "$location", "$state", "$rootScope", "$window", "$cookies", "usSpinnerService", "Idle", "Keepalive", "$uibModal","usersHttpService", userProfileController]);
}())