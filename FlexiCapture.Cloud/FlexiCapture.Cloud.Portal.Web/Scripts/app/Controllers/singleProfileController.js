(function () {
    var singleProfileController = function ($scope, $http, $timeout, $location, $state, $rootScope, $window, $cookies, usSpinnerService, Idle, Keepalive, $uibModal, manageUserProfileHttpService) {
        var data = [];
        $scope.changeCount = 0;
        var url = $$ApiUrl + "/userProfile";
        $scope.profileIsChanged = false;
        var singleProfile = function () {
            $scope.loadData = true;
            manageUserProfileHttpService.getToUserProfiles($http, $scope, $state, data, url, usSpinnerService);
            // $scope.loadData = false;
            $scope.profileIsChanged = false;

        };
        singleProfile();

        $scope.updateSettings = function () {
            //alert(JSON.stringify($scope.currentProfile));
            
            var isEdit = $scope.profileIsChanged;
            manageUserProfileHttpService.manageProfile($http, $scope, data, url, usSpinnerService, isEdit);

            $scope.profileIsChanged = false;
        }




        $scope.changeProfile = function () {

            $scope.changeCount = 0;
            $scope.profileIsChanged = false;

        };

        $scope.$watch('$viewContentLoaded',
            function () {
                $timeout(function () {
                    //do something
                    $scope.changeCount = 0;
                    $scope.profileIsChanged = false;
                }, 0);
            });
        $scope.$watchCollection('currentProfile', function () {

            if ($scope.changeCount > 0)
                $scope.profileIsChanged = true;
            $scope.changeCount++;

        });

        $scope.imChanged = function () {
            if ($scope.changeCount > 0)
                $scope.profileIsChanged = true;
            $scope.changeCount++;
                
        }
    };



    fccApp.controller("singleProfileController", ["$scope", "$http", "$timeout", "$location", "$state", "$rootScope", "$window", "$cookies", "usSpinnerService", "Idle", "Keepalive", "$uibModal", "manageUserProfileHttpService", singleProfileController]);
}())