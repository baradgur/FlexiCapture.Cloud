(function () {
    var emailProfileController = function ($scope, $http, $timeout, $location, $state, $rootScope, $window, $cookies, $filter, usSpinnerService, Idle, Keepalive, $uibModal, manageUserProfileHttpService) {
        var data = [];
        $scope.profiles = [];
        $scope.changeCount = 0;
        $scope.changeNameCount = 0;
        $scope.currentProfile = {};
        var url = $$ApiUrl + "/userProfile";
        $scope.NewProfileName = "";
        $scope.newProfile = false;
        $scope.profileIsChanged = false;
        $scope.defaultProfileId = -1;
        $scope.oldDefaultProfileId = -1;
        $scope.searchLangText = "";
        var emailProfile = function () {
            $scope.loadData = true;
            manageUserProfileHttpService.getToUserProfiles($http, $scope, url, usSpinnerService, 0);
            // $scope.loadData = false;
            $scope.profileIsChanged = false;

        };
        emailProfile();

        $scope.changeDefaultProfile = function () {
            $scope.profileIsChanged = false;

            $scope.currentProfile.isDefault = true;
            for (var i = 0; i < $scope.profiles.length; i++) {
                if ($scope.profiles[i].Id == $scope.currentProfile.Id) {
                    $scope.profiles[i].isDefault = true;
                    $scope.defaultProfileId = $scope.profiles[i].Id;
                } else {
                    $scope.profiles[i].isDefault = false;

                }
            };

            var url = $$ApiUrl + "/defaultProfile";
            manageUserProfileHttpService.updateDefaultProfile($http, $scope, url, usSpinnerService);
        }
        $scope.showNewProfile = function (show) {

            $scope.newProfile = show;
            $scope.NewProfileName = "";

        }

        $scope.safeApply = function (fn) {
            var phase = this.$root.$$phase;
            if (phase == '$apply' || phase == '$digest') {
                if (fn && (typeof (fn) === 'function')) {
                    fn();
                }
            } else {
                this.$apply(fn);
            }
        };

        $scope.createNewSettings = function () {
            var isEdit = false;
            manageUserProfileHttpService.manageProfile($http, $scope, data, url, usSpinnerService, isEdit);
            $scope.profileIsChanged = false;
            $scope.profileIsChanged = false;
            $scope.showNewProfile(false);
        }
        $scope.updateSettings = function () {
            //alert(JSON.stringify($scope.profiles));
            //$scope.currentProfile = {};
            var isEdit = $scope.profileIsChanged;
            $scope.currentProfile.Name = $scope.currentProfile.Name.replace(" [Settings Were Changed]", "");
            manageUserProfileHttpService.manageProfile($http, $scope, data, url, usSpinnerService, isEdit);
            $scope.showNewProfile(false);
            $scope.profileIsChanged = false;
        }




        $scope.changeProfile = function () {


            for (var i = 0; i < $scope.profiles.length; i++) {
                if ($scope.currentProfile.Id == $scope.profiles[i].Id) {
                    $scope.currentProfile = angular.copy($scope.profiles[i]);
                    $scope.defaultProfileId = $scope.currentProfile.Id;
                }
                $scope.profiles[i].Name = $scope.profiles[i].Name.replace(" [Settings Were Changed]", "");
            }


            $scope.changeCount = 0;
            $scope.changeNameCount = 0;
            $scope.profileIsChanged = false;

        };

        $scope.$watch('$viewContentLoaded',
            function () {
                $timeout(function () {
                    //do something
                    $scope.changeCount = 0;
                    $scope.changeNameCount = 0;
                    $scope.profileIsChanged = false;
                }, 0);
            });
        $scope.$watchCollection('currentProfile', function () {

            if ($scope.changeCount > 0) {
                $scope.profileIsChanged = true;
                var found = $filter('filter')($scope.profiles, { Id: $scope.currentProfile.Id }, true);
                if (found.length > 0 && $scope.changeNameCount == 0) {
                    found[0].Name += " [Settings Were Changed]";
                    $scope.changeNameCount++;
                }
            }
            $scope.changeCount++;

        });

        $scope.imChanged = function () {
            if ($scope.changeCount > 0)
                $scope.profileIsChanged = true;
            $scope.changeCount++;

        }
    };



    fccApp.controller("emailProfileController", ["$scope", "$http", "$timeout", "$location", "$state", "$rootScope", "$window", "$cookies", "$filter", "usSpinnerService", "Idle", "Keepalive", "$uibModal", "manageUserProfileHttpService", emailProfileController]);
}())