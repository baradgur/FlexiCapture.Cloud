var addPlanController = function ($scope, $http, $location, $state, $uibModal, $uibModalInstance, $stateParams, usSpinnerService, documentsHttpService, items) {
    var vm = this;

    function loadSettingData() {
        vm.$scope = $scope;
        vm.items = items;
        vm.plans = $scope.plans;
        $scope.submitted = false;
        vm.confirm = function () {
            if ($scope.settingForm.$valid) {
                $scope.submitted = false;
                $uibModalInstance.close();
            }
            else { $scope.submitted = true; }
        }
        vm.cancel = $uibModalInstance.dismiss;
    }
    loadSettingData();

    vm.name = "";
    vm.autoRenewal = 1;
    vm.planExpiration = 0;
    vm.cost = 0;
    vm.pagesInInterval = 0;

    //{
    //    name: "First",
    //    autoRenewal: 1, // one time
    //    planExpiration: 60,
    //    cost: 0,
    //    pagesInInterval: 5000
    //}

    vm.addPlan = function() {
        vm.item = 
              {
                  name: vm.name,
                  autoRenewal: vm.autoRenewal, // one time
                  planExpiration: vm.planExpiration,
                  cost: vm.cost,
                  pagesInInterval: vm.pagesInInterval
              }

        vm.$scope.plans.push(vm.item);
        $("#table").bootstrapTable("load", vm.$scope.plans);
    }



    $("#apply-plan").on('click',
        function() {
            vm
        });


};
fccApp.controller("addPlanController", ["$scope", "$http", "$location", "$state", "$uibModal", "$uibModalInstance", "$stateParams", "usSpinnerService", "documentsHttpService", addPlanController]);