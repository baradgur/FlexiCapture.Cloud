fccApp.service('communicationHttpService', function () {
    //добавляем данные communication для таблицы
    function addData(model) {
        var dElement = {};
        dElement.type = model.NotificationTypeId == 1 ? "Important" : model.NotificationTypeId == 2
            ? "Portal updates and new releases" : model.NotificationTypeId == 3 ? "Monthly use & payment summary" : "";
        dElement.subject = model.Subject;
        dElement.message = model.Message;
        dElement.sender = model.Sender.FirstName + " " + model.Sender.LastName;
        dElement.acceptor = model.User ? model.User.FirstName + " " + model.User.LastName : model.UserRoleId == 0 ?
            "All Users" : model.UserRoleId == 2 ? "Account Owners" : model.UserRoleId == 3 ? "Operators" :
            model.UserRoleId == 4 ? "Viewers" : "";

        if (moment.utc(model.Date).isValid()) {
            dElement.date = moment.utc(model.Date);
            dElement.date.local();
            dElement.date = dElement.date.format("YYYY-MM-DD HH:mm");
        }

        return dElement;
    }

    //send messages
    this.sendCommunication = function ($http, $scope, url, data, usSpinnerService) {

        usSpinnerService.spin("spinner-1");

        $http({
            url: url,
            method: "POST",
            contentType: "application/json",
            data: JSON.stringify($scope.communication)
        })
            .then(function (response) {

                usSpinnerService.stop('spinner-1');
                if (response.data != null) {
                    showNotify("Success", "Notification was successfully sent", "success");
                    $scope.communications.push(response.data);
                    var dElement = addData(response.data);
                    data.push(dElement);
                    $('#table').bootstrapTable('load', data);
                    $('#table').bootstrapTable('resetWidth');
                } else {
                    showNotify("Danger", "Error occured", "danger");
                }
                $('#table').bootstrapTable('resetWidth');
            },
                function (response) { // optional
                    // failed
                    usSpinnerService.stop('spinner-1');
                    showNotify("Danger", "Error occurred, please contact administrator", "danger");
                });
    }

    this.getCommunication = function ($http, $scope, data, url, usSpinnerService) {

        $scope.communications = [];
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

                        $scope.communications.push(comm);
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