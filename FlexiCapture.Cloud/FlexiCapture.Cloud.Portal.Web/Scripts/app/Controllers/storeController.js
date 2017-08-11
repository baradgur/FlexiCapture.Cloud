(function() {
    var storeController = function($scope, $http, $location, $state, $rootScope, $window, $cookies, usSpinnerService, Idle, Keepalive, $uibModal, storeHttpService) {
        var url = $$ApiUrl + "/store";

        var store = function() {

            $scope.loadData = false;

        };
        store();


        $scope.setServiceState = function(serviceId) {
            var serviceState = {};
            serviceState.ServiceId = serviceId;
            serviceState.UserId = $scope.userData.UserData.Id;
            switch (serviceId) {
                case 2:
                    serviceState.State = !$scope.userData.ServiceData.BatchFileConversionService;
                    storeHttpService.setServiceState($http, $scope, $window, url, usSpinnerService, serviceState);
                    break;

                case 4:
                    serviceState.State = !$scope.userData.ServiceData.EmailAttachmentFileConversionService;
                    storeHttpService.setServiceState($http, $scope, $window, url, usSpinnerService, serviceState);
                    break;

                case 3:
                    serviceState.State = !$scope.userData.ServiceData.FTPFileConversionService;
                    storeHttpService.setServiceState($http, $scope, $window, url, usSpinnerService, serviceState);
                    break;

                case 5:
                    serviceState.State = !$scope.userData.ServiceData.OnlineWebOcrApiService;
                    storeHttpService.setServiceState($http, $scope, $window, url, usSpinnerService, serviceState);
                    break;

            }
        }

    };


    fccApp.controller("storeController", ["$scope", "$http", "$location", "$state", "$rootScope", "$window", "$cookies", "usSpinnerService", "Idle", "Keepalive", "$uibModal", "storeHttpService", storeController]);
}())