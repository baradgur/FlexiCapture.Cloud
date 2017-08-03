fccApp.service('onlineWebOcrSettingsHttpService', function () {

    function addData(apikey) {
        var dElement = {};
        dElement.keyId = apikey.Id;
        dElement.viewId = 110000 + apikey.Id;
        dElement.keyKey = apikey.Key;
        dElement.keyState = apikey.IsActive?"Active":"Disabled";
        return dElement;
    }

    //get to clients list
    this.getToOcrApiKeys = function ($http, $scope, $state, url, data, usSpinnerService) {
        $scope.loading = true;
        usSpinnerService.spin("spinner-1");

        $http.get(url, {
            params: { id: $scope.userData.UserData.Id }
        })

        .then(function (response) {
            
            if (response.data != null) {
                for (var i = 0; i < response.data.length; i++) {
                    var apikey = {};

                    apikey = response.data[i];

                    $scope.apiKeys.push(apikey);
                    var dElement = addData(apikey);
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
        });
    }

    //add or Edit user
    this.manageKey = function ($http, $scope, data, url, usSpinnerService, isEdit) {

        usSpinnerService.spin("spinner-1");
        var methodType = "POST";
        if (isEdit) {
            methodType = "PUT";
        }

        $http({
            url: url,
            method: methodType,
            contentType: "application/json",
            data: JSON.stringify($scope.apiKey)
        })
            .then(function (response) {
                
                usSpinnerService.stop('spinner-1');
                $scope.loadData = false;
                var apiKey = response.data;

                if (apiKey != null) {
                    var dElement = addData(apiKey);
                    if (!isEdit) {
                        data.push(dElement);
                        $scope.apiKeys.push(apiKey);
                    } else {
                        for (var i = 0; i < data.length; i++) {

                            var obj = data[i];
                            if (obj.keyId == dElement.keyId) {
                                data[i] = dElement;
                                break;
                            }
                        }
                        for (var j = 0; j < $scope.apiKeys.length; j++) {
                            if (apiKey.Id == $scope.apiKeys[j].Id) {
                                $scope.apiKeys[j] = apiKey;
                                break;
                            }
                        }
                    }
                    $('#table').bootstrapTable('load', data);
                    var notificationT = "added";
                    if (isEdit) notificationT = "updated";

                    showNotify("Успех", "Key was successfully " + notificationT, "success");
                    $('#table').bootstrapTable('resetWidth');
                    // success
                }
            },
                function (response) { // optional
                    // failed
                    usSpinnerService.stop('spinner-1');
                    var notificationT = "adding";
                    if (isEdit) notificationT = "updating";
                    showNotify("Успех", "Error occurred while "+notificationT+" key", "danger");
                    $('#table').bootstrapTable('resetWidth');
                });
        $('#table').bootstrapTable('resetWidth');

    }
});