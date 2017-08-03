fccApp.service('logHttpService', function () {
    //добавляем данные log для таблицы
    function addData(model) {
        var dElement = {};
        dElement.id = model.Id;
        dElement.description = model.Message;
        if (moment.utc(model.Date).isValid()) {
            dElement.date = moment.utc(model.Date);
            dElement.date.local();
            dElement.date = dElement.date.format("YYYY-MM-DD HH:mm");
        }

        return dElement;
    }

    this.getLog = function ($http, $scope, data, url, usSpinnerService) {

        $scope.logs = [];
        usSpinnerService.spin("spinner-1");

        $http({
            url: url,
            type: 'GET'
        })
            .then(function (response) {
                //$scope.userData.UserData
                if (response.data != null) {
                    for (var i = 0; i < response.data.length; i++) {
                        var comm = {};

                        comm = response.data[i];

                        $scope.logs.push(comm);
                        var dElement = addData(comm);
                        data.push(dElement);
                    }
                }

                $('#table').bootstrapTable({
                    data: data,
                    height: '100%',
                    onPostBody: function () {
                        $('#table').bootstrapTable('resetView');
                    }

                });

                $('[data-toggle="tooltip"]').tooltip();

                var $result = $('#eventsResult');

                $('#table').on('all.bs.table',
                        function (e, name, args) {
                            // console.log('Event:', name, ', data:', args);
                        })
                    .on('click-row.bs.table',
                        function (e, row, $element) {
                            // $result.text('Event: click-row.bs.table'+ JSON.stringify(row.userName));
                        });

                $('#table').bootstrapTable('resetWidth');


                $scope.loading = false;
                usSpinnerService.stop('spinner-1');
                window.scope = $scope;
            });
    }
});