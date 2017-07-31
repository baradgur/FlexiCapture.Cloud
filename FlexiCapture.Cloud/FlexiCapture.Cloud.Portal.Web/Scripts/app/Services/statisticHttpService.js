fccApp.service('statisticHttpService', function() {

    //get to clients list
    this.getToDefaultStatisticParams = function($http, $scope, $state, url, usSpinnerService) {
        $scope.loading = true;
        $scope.user = {};
        usSpinnerService.spin("spinner-1");

        $http.get(url, {
            params: { id: -1 }
        })

        .then(function(response) {
            //alert(JSON.stringify(response.data));
            // $scope.currentUser = JSON.parse(response.data);
            // $scope.user = angular.copy($scope.currentUser);
            $scope.statisticModel = JSON.parse(response.data);
            $scope.statisticModel.ShowDateRange = false;
            $scope.statisticModel.StartDate = new Date($scope.statisticModel.StartDate);
            $scope.statisticModel.EndDate = new Date($scope.statisticModel.EndDate);
            $scope.showChart = false;
            usSpinnerService.stop('spinner-1');
            $scope.loading = false;
            //window.scope = $scope;
        });
    }

    //add or Edit user
    this.showStatistic = function($http, $scope, url, usSpinnerService) {


        usSpinnerService.spin("spinner-1");
        var methodType = "POST";
        // alert(JSON.stringify($scope.statisticModel));
        $http({
                url: url,
                method: methodType,
                contentType: "application/json",
                data: JSON.stringify($scope.statisticModel)
            })
            .then(function(response) {
                    usSpinnerService.stop('spinner-1');
                    //$scope.loadData = false;
                    $scope.showChart = true;
                    $scope.statistic = JSON.parse(response.data);
                    $scope.callDrawChart();

                },
                function(response) { // optionalr
                    // failed
                    usSpinnerService.stop('spinner-1');
                    showNotify("Error", "Error getting statistics", "danger");

                });
    }
});