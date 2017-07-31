    var statisticController = function($scope, $http, $location, $state, $rootScope, $window, $cookies, usSpinnerService, Idle, Keepalive, $uibModal, statisticHttpService) {
        var url = $$ApiUrl + "/statistic";


        //
        var statistic = function() {
            $scope.loadData = false;
            statisticHttpService.getToDefaultStatisticParams($http, $scope, $state, url, usSpinnerService);

        };
        statistic();

        $scope.drawChart = function() {
            var data = google.visualization.arrayToDataTable($scope.statistic.ChartData);

            var options = {
                title: 'Statistic',
                hAxis: { title: 'Dates', titleTextStyle: { color: '#333' } },
                vAxis: { minValue: 0 }
            };
            $scope.showChart = true;
            var chart = new google.visualization.AreaChart(document.getElementById('chart_div'));
            google.visualization.events.addListener(chart, 'ready', $scope.afterDraw);

            chart.draw(data, options);


        }

        $scope.afterDraw = function() {
            document.getElementById('chart_div').style.height = '300px';
            document.getElementById('chart_div').style.width = '100%';

            usSpinnerService.stop('spinner-1');
            console.log('all done');
        }

        $scope.callDrawChart = function() {
            google.charts.load('current', { 'packages': ['corechart'] });
            google.charts.setOnLoadCallback($scope.drawChart);
        }

        $scope.changeDateRange = function() {
            if ($scope.statisticModel.DateRange.Id == 4) {
                $scope.statisticModel.ShowDateRange = true;
            } else {
                $scope.statisticModel.ShowDateRange = false;
            }
        }

        $scope.showStatistic = function() {
            statisticHttpService.showStatistic($http, $scope, url, usSpinnerService);
        }
    };


    fccApp.controller("statisticController", ["$scope", "$http", "$location", "$state", "$rootScope", "$window", "$cookies", "usSpinnerService", "Idle", "Keepalive", "$uibModal", "statisticHttpService", statisticController]);