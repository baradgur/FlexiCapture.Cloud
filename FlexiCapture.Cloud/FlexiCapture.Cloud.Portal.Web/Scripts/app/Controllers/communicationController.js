var communicationController = function ($scope, $http, $location, $state, $window, $uibModal, $stateParams, usSpinnerService, communicationHttpService) {
    var url = $$ApiUrl + "/Communication";

    $scope.userData = JSON.parse($window.sessionStorage.getItem("UserData"));

    $scope.loading = true;
    var data = [];

    $scope.communication = { UserRoleId: 0, NotificationTypeId: 1 };

    $scope.availableOptions = [{ Id: 2, Name: "Account Owners" }, { Id: 3, Name: "Operators" },
        { Id: 4, Name: "Viewers" }, { Id: 0, Name: "All Users" }, { Id: -1, Name: "Single User" }];

    $scope.selectedOption = { Id: 0, Name: "All Users" };

    $scope.loadCommunicationData = function () {
        communicationHttpService.getCommunication($http, $scope, data, url, usSpinnerService);
    }

    $scope.getUser = function (val) {
        return $http.get(url,
        {
            params: {
                userValue: val
            }
        }).then(function (response) {
            if (response.data != null) {
                for (var i = 0; i < response.data.length; i++) {
                    response.data[i].UserInfo = response.data[i].FirstName +
                        " " +
                        response.data[i].LastName +
                        ", " +
                        response.data[i].Email +
                        ";";
                }
            }
            return response.data;
        });
    }

    $scope.sendCommunication = function() {
        if ($scope.communicationForm.$valid) {
            $scope.submitted = false;
            $scope.communication.UserRoleId = $scope.selectedOption.Id;
            $scope.communication.Sender = angular.copy($scope.userData.UserData);
            console.log($scope.communication);
            communicationHttpService.sendCommunication($http, $scope, url, data, usSpinnerService);
        } else {
            $scope.submitted = true;
        }
        
    }

    $scope.loadCommunicationData();


};
fccApp.controller("communicationController", ["$scope", "$http", "$location", "$state", "$window", "$uibModal", "$stateParams", "usSpinnerService", "communicationHttpService", communicationController]);