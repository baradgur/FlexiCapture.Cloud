fccApp.service('emailSettingsHttpService', function () {
    //добавляем данные user в для таблицы
    function addData(setting) {
        var dElement = {};
        dElement.settingId = setting.Id;
        dElement.settingUserId = setting.UserId;
        dElement.settingAccountName = setting.AccountName;
        dElement.settingHost = setting.Host;
        dElement.settingPort = setting.Port;
        dElement.settingPassword = setting.Password;
        dElement.settingEmail = setting.Email;
        dElement.settingUseSSL = setting.UseSSL;

        return dElement;
    }


    //get to clients list
    this.getToEmailSettingsList = function ($http, $scope, $state, data, url, usSpinnerService) {
        $scope.settings = [];
        usSpinnerService.spin("spinner-1");

        $http.get(url, {
            params: { userId: $scope.choosedUserId ? $scope.choosedUserId : $scope.userData.UserData.Id }
        }).then(function (response) {
            if (response.data.Error) {
                BootstrapDialog.show({
                    title: response.data.Error.Name,
                    type: BootstrapDialog.TYPE_WARNING,
                    cssClass: 'bp-z',
                    message: response.data.Error.ShortDescription + "</br>" + response.data.Error.FullDescription
                });
            }
            else {
                for (var i = 0; i < response.data.Settings.length; i++) {
                    var setting = {};

                    setting = response.data.Settings[i];

                    $scope.settings.push(setting);

                    $scope.loading = true;
                    var dElement = addData(setting);
                    data.push(dElement);
                }
            }
            $('#emailSettingstable').bootstrapTable({
                data: data,
                height: '100%',
                onPostBody: function () {
                    $('#emailSettingstable').bootstrapTable('resetView');
                }

            });

            $('[data-toggle="tooltip"]').tooltip();

            var $result = $('#eventsResult');

            $('#emailSettingstable').on('all.bs.table',
                    function (e, name, args) {
                        // console.log('Event:', name, ', data:', args);
                    })
                .on('click-row.bs.table',
                    function (e, row, $element) {
                        // $result.text('Event: click-row.bs.table'+ JSON.stringify(row.settingName));
                    });

            $('#emailSettingstable').bootstrapTable('resetWidth');

            usSpinnerService.stop('spinner-1');
            $scope.loading = false;
            window.scope = $scope;
        });
    }

    //add or Edit setting
    this.manageSetting = function ($http, $scope, data, url, usSpinnerService, isEdit) {

        usSpinnerService.spin("spinner-1");
        var methodType = "POST";
        if (isEdit) {
            methodType = "PUT";
        }

        $http({
            url: url,
            method: methodType,
            contentType: "application/json",
            data: JSON.stringify($scope.setting)
        })
            .then(function (response) {
                usSpinnerService.stop('spinner-1');
                $scope.loadData = false;
                var setting = response.data;

                if (setting.Error) {
                    BootstrapDialog.alert({
                        title: setting.Error.Name,
                        type: BootstrapDialog.TYPE_WARNING,
                        message: setting.Error.ShortDescription + "</br>" + setting.Error.FullDescription
                    });
                }
                else {
                    var dElement = addData(setting);
                    if (!isEdit) {
                        data.push(dElement);
                        $scope.settings.push(setting);
                    } else {
                        for (var i = 0; i < data.length; i++) {

                            var obj = data[i];
                            if (obj.settingId == dElement.settingId) {
                                data[i] = dElement;
                                break;
                            }
                        }
                        for (var j = 0; j < $scope.settings.length; j++) {
                            if (setting.Id == $scope.settings[j].Id) {
                                $scope.settings[j] = setting;
                                break;
                            }
                        }
                    }
                    $('#emailSettingstable').bootstrapTable('load', data);
                    var notificationT = "added";
                    if (isEdit) notificationT = "updated";

                    showNotify("Успех", "email setting was successfully " + notificationT, "success");
                    $('#emailSettingstable').bootstrapTable('resetWidth');
                    // success
                }
            },
            function (response) { // optional
                // failed
                usSpinnerService.stop('spinner-1');
                showNotify("Успех", "Error while updating setting", "danger");
                $('#emailSettingstable').bootstrapTable('resetWidth');
            });
        $('#emailSettingstable').bootstrapTable('resetWidth');

    }
});