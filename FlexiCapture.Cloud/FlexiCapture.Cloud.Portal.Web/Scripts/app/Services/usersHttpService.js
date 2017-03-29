fccApp.service('usersHttpService', function () {
    //добавляем данные user в для таблицы
    function addData(user) {
        var dElement = {};
        dElement.userId = user.UserData.Id;
        dElement.userName = user.UserData.FirstName;
        dElement.userLastName = user.UserData.LastName;
        dElement.roleName = user.UserRoleData.ShortDescription;
        dElement.lastActivityDate = user.UserData.LastActivityDate;
        
        
        return dElement;
    }


    //get to clients list
    this.getToUsersList = function ($http, $scope, $state, data, url, usSpinnerService) {
        $scope.users = [];
        usSpinnerService.spin("spinner-1");

        $http.get(url).then(function (response) {

            for (i = 0; i < response.data.UsersData.length; i++) {
                var user = {};

                user = response.data.UsersData[i];

                $scope.users.push(user);

                $scope.loading = true;
                var dElement = addData(user);
                data.push(dElement);
            }
            $scope.availableRoles = response.data.UserRolesData;
            $scope.availableCompanies = response.data.CompaniesData;
            $scope.availableCompanyUnits = response.data.CompanyUnitsData;
            $scope.availableLoginStates = response.data.LoginStatesData;

            $('#table').bootstrapTable({
                data: data,
                height: '100%',
                onPostBody: function () {
                    $('#table').bootstrapTable('resetView');
                }

            });

            $('[data-toggle="tooltip"]').tooltip()

            var $result = $('#eventsResult');

            $('#table').on('all.bs.table', function (e, name, args) {
                // console.log('Event:', name, ', data:', args);
            })
                .on('click-row.bs.table', function (e, row, $element) {
                    // $result.text('Event: click-row.bs.table'+ JSON.stringify(row.userName));
                })

            $('#table').bootstrapTable('resetWidth');
            usSpinnerService.stop('spinner-1');
            $scope.loading = false;
            window.scope = $scope;
        });
    }

    //add or Edit user
    this.manageUser = function ($http, $scope, data, url, usSpinnerService, isEdit) {


        usSpinnerService.spin("spinner-1");
        var methodType = "POST";
        if (isEdit) {
            methodType = "PUT";
        }

        $http({
            url: url,
            method: methodType,
            contentType: "application/json",
            data: JSON.stringify($scope.user)
        })
            .then(function (response) {
                usSpinnerService.stop('spinner-1');
                $scope.loadData = false;
                console.log(JSON.stringify(response.data));
                var user = JSON.parse(response.data);
                if (user.Error != null) {
                    BootstrapDialog.alert({
                        title: user.Error.Name,
                        type: BootstrapDialog.TYPE_WARNING,
                        message: user.Error.ShortDescription + "</br>" + user.Error.FullDescription
                    });
                }
                else {
                    var dElement = addData(user);
                    if (!isEdit) {
                        data.push(dElement);
                        $scope.users.push(user);
                    } else {
                        for (var i = 0; i < data.length; i++) {

                            var obj = data[i];
                            if (obj.userId == dElement.userId) {
                                data[i] = dElement;
                                break;
                            }
                        }
                        for (var j = 0; j < $scope.users.length; j++) {
                            if (user.UserData.Id == $scope.users[j].UserData.Id) {
                                $scope.users[j] = user;
                                break;
                            }
                        }
                    }
                    $('#table').bootstrapTable('load', data);
                    var notificationT = "добавлен";
                    if (isEdit) notificationT = "обновлен";

                    showNotify("Успех", "Пользователь " + $scope.user.UserData.FirstName + " был успешно " + notificationT, "success");
                    $('#table').bootstrapTable('resetWidth');
                    // success
                }
            },
            function (response) { // optional
                // failed
                usSpinnerService.stop('spinner-1');
                showNotify("Успех", "Ошибка при добавлении пользователя", "danger");
                $('#table').bootstrapTable('resetWidth');
            });
        $('#table').bootstrapTable('resetWidth');
    }
});