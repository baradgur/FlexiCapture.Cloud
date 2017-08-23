fccApp.service('ftpSettingsHttpService', function () {
    //добавляем данные user в для таблицы
    function addData(setting) {
        var dElement = {};
        dElement.settingId = setting.InputFtpSettingsModel.Id;
        dElement.settingUserId = setting.InputFtpSettingsModel.UserId;
        dElement.settingUserName = setting.InputFtpSettingsModel.UserName;
        dElement.settingHost = setting.InputFtpSettingsModel.Host;
        dElement.settingPort = setting.InputFtpSettingsModel.Port;
        dElement.settingPassword = setting.InputFtpSettingsModel.Password;
        dElement.settingPath = setting.InputFtpSettingsModel.Path;
        dElement.settingUseSSL = setting.InputFtpSettingsModel.UseSSL;
        dElement.settingCustomOutExc = (setting.OutputFtpSettingsModel == null && setting.ExceptionFtpSettingsModel)
            ? "No"
            : "Yes";
        dElement.settingDeleteFile = setting.InputFtpSettingsModel.DeleteFile;
        dElement.settingEnabled = setting.InputFtpSettingsModel.Enabled;

        return dElement;
    }

    this.testFtpAccess = function ($http, $scope, $state, data, url, usSpinnerService) {
        $http.post(
            url,
            JSON.stringify($scope.setting),
            {
                headers: {
                    'Content-Type': 'application/json'
                }
            }
        ).then(function (resp) {
            $scope.saveButtonAvailable = true;
            showNotify("Tested successful", "FTP settings are valid", "success");
            $scope.showSaveButton = true;
        },
        function (resp) {
            switch (resp.data.Message) {
                case "1":
                    showNotify("Setting not valid", "Input setting is not valid", "danger");
                    break;
                case "2":
                    showNotify("Setting not valid", "Output setting is not valid", "danger");
                    break;
                case "3":
                    showNotify("Setting not valid", "Exception setting is not valid", "danger");
                    break;
            }
        });
    }

    this.getFtpConversionSettings = function ($http, $scope, $state, data, url, usSpinnerService) {
        $http.get(url,
        {
            params: { userId: $scope.userData.UserData.Id }
        }).then(function (response) {
            $scope.ftpConversionSetting = response.data;

        });
    }

    this.addFtpConversionSettings = function ($http, $scope, $state, data, url, usSpinnerService) {
        $http({
            url: url,
            method: "POST",
            contentType: "application/json",
            data: JSON.stringify($scope.responseSetting)
        }).then(function (response) {
            if (response.data != null) {
                $scope.ftpConversionSetting = response.data;
                $scope.ftpConversionSettingToCompare = angular.copy($scope.ftpConversionSetting);
                showNotify("Успех", "Setting were successfully updated", "success");
            } else {
                showNotify("Ошибка", "Problems while updating settings", "danger");
            }
        });
    }


    //get to clients list
    this.getToFTPSettingsList = function ($http, $scope, $state, data, url, usSpinnerService) {
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
                    message: /*response.data.Error.ShortDescription + "</br>" +*/ response.data.Error.FullDescription
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
            $('#ftpSettingstable').bootstrapTable({
                data: data,
                height: '100%',
                onPostBody: function () {
                    $('#ftpSettingstable').bootstrapTable('resetView');
                }

            });

            if (response.data.Settings.length == 0) {
                $("#e-message")
                    .text("FTP is not configured yet. Please click [+] " +
                    "button above to configure FTP Settings.");
            }

            $('[data-toggle="tooltip"]').tooltip();

            var $result = $('#eventsResult');

            $('#ftpSettingstable').on('all.bs.table',
                    function (e, name, args) {
                        // console.log('Event:', name, ', data:', args);
                    })
                .on('click-row.bs.table',
                    function (e, row, $element) {
                        // $result.text('Event: click-row.bs.table'+ JSON.stringify(row.settingName));
                    });

            $('#ftpSettingstable').bootstrapTable('resetWidth');

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
                    $('#ftpSettingstable').bootstrapTable('load', data);
                    var notificationT = "added";
                    if (isEdit) notificationT = "updated";

                    showNotify("Успех", "FTP setting was successfully " + notificationT, "success");
                    $('#ftpSettingstable').bootstrapTable('resetWidth');
                    // success
                }
            },
            function (response) { // optional
                // failed
                usSpinnerService.stop('spinner-1');
                showNotify("Успех", "Error while updating setting", "danger");
                $('#ftpSettingstable').bootstrapTable('resetWidth');
            });
        $('#ftpSettingstable').bootstrapTable('resetWidth');

    }
});