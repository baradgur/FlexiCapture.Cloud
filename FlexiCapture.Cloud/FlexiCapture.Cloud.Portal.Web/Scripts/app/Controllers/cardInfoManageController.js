var cardInfoManageController = function ($scope, $http, $location, $state, $uibModal, $uibModalInstance, $stateParams, usSpinnerService) {
    var vm = this;
    

    function loadInfo() {
        vm.$scope = $scope;
        vm.user = $scope.user;
        $scope.submitted = false;
        vm.confirm = function (form) {
            if ($scope.cardForm.$valid) {
                $scope.submitted = false;
                $scope.save($scope.cardInfo);
                $uibModalInstance.close();
            }
            else { $scope.submitted = true; }
        }
        
        vm.cancel = $uibModalInstance.dismiss;
    }
    loadInfo();

};



fccApp.controller("cardInfoManageController", ["$scope", "$http", "$location", "$state", "$uibModal", "$stateParams", "usSpinnerService", cardInfoManageController]);
