var addPlanController = function ($scope, $http, $location, $state, $uibModal, $uibModalInstance, $stateParams, usSpinnerService, documentsHttpService, items) {
    var vm = this;

    function loadData() {
        vm.$scope = $scope;
        $scope.submitted = false;
        vm.confirm = function () {
            if ($scope.addPlanForm.$valid) {
                $scope.submitted = false;
                $uibModalInstance.close();
            }
            else { $scope.submitted = true; }
        }
        vm.cancel = $uibModalInstance.dismiss;
    }
    loadData();
    
};
fccApp.controller("addPlanController", ["$scope", "$http", "$location", "$state", "$uibModal", "$uibModalInstance", "$stateParams", "usSpinnerService", "documentsHttpService", addPlanController]);