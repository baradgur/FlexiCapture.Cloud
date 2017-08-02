fccApp.service('usersHttpService', function ($filter) {
    //добавляем данные user в для таблицы
    function addData(user) {
        var dElement = {};
        dElement.userId = user.UserData.Id;
        dElement.viewId = 110000 + user.UserData.Id;
        dElement.userName = user.UserData.FirstName;
        dElement.userLastName = user.UserData.LastName;
        dElement.roleName = user.UserRoleData.ShortDescription;
        dElement.lastActivityDate = user.UserData.LastActivityDate;
        dElement.phoneNum = user.UserData.PhoneNumber;
        dElement.parentName = user.UserData.ParentUserName;
        dElement.regDate = user.LoginData.RegistrationDate;
        dElement.companyName = user.UserData.CompanyName;
        dElement.email = user.UserData.Email;


        return dElement;
    }


    //get to clients list
    this.getToUsersList = function ($http, $scope, $state, data, url, usSpinnerService) {

        $scope.users = [];
        usSpinnerService.spin("spinner-1");
        var parameters = { userId: $scope.userData.UserData.Id, userRoleId: $scope.userData.UserData.UserRoleId};

        $http({
            url: url,
            type: 'GET',
            params: parameters
        })
            .then(function (response) {
                //$scope.userData.UserData
                for (var i = 0; i < response.data.UsersData.length; i++) {
                    var user = {};

                    user = response.data.UsersData[i];

                    $scope.users.push(user);
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

    //add or Edit user
    this.manageUser = function (callback, $http, $scope, data, url, usSpinnerService, isEdit) {
        var responseSuccess = null;
        var responseNoError = null;

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
                responseSuccess = true;
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
                } else {
                    responseNoError = true;
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
                    var notificationT = "added";
                    if (isEdit) notificationT = "updated";

                    showNotify("Успех", "User " + $scope.user.UserData.FirstName + " was successfully" + notificationT, "success");
                    $('#table').bootstrapTable('resetWidth');
                    // success
                }
                callback(responseSuccess, responseNoError);
            },
                function (response) { // optional
                    // failed
                    usSpinnerService.stop('spinner-1');
                    showNotify("Успех", "Error occurred while adding user", "danger");
                    $('#table').bootstrapTable('resetWidth');
                    callback(responseSuccess, responseNoError);
                });
        $('#table').bootstrapTable('resetWidth');

    }

    //get to clients list
    this.getToUserProfile = function ($http, $scope, $state, url, usSpinnerService) {
        $scope.user = {};
        usSpinnerService.spin("spinner-1");

        $http.get(url, {
            params: { userId: $scope.userData.UserData.Id }
        })

        .then(function (response) {
            // alert(JSON.stringify(response.data));
            $scope.currentUser = JSON.parse(response.data);
            $scope.user = angular.copy($scope.currentUser);
            usSpinnerService.stop('spinner-1');
            $scope.loading = false;
            window.scope = $scope;
        });
    }

    //edit user profile
    this.updateUserProfile = function ($http, $scope, url, usSpinnerService) {


        usSpinnerService.spin("spinner-1");
        var methodType = "PUT";

        $http({
            url: url,
            method: methodType,
            contentType: "application/json",
            data: JSON.stringify($scope.currentUser)
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
                } else {
                    $scope.user = user;
                    showNotify("Успех", user.FirstName + "! Your profile has been successfully updated", "success");
                    // success
                }
            },
                function (response) { // optional
                    // failed
                    usSpinnerService.stop('spinner-1');
                    showNotify("Успех", "Unable to update profile", "danger");
                });
    };

    //delete by ID
    this.deleteUser = function ($http, $scope, usSpinnerService, url, rParams, data) {


        usSpinnerService.spin("spinner-1");

        $http.delete(url, {
            params: rParams
        })
            .success(function (result) {
                if (result != null) {
                    data.splice(0, data.length);
                    usSpinnerService.stop('spinner-1');
                    showNotify("Success", "Data deleted successfully", "success");
                    for (var i = 0; i < result.length; i++) {
                        var index = -1;
                        for (var j = 0; j < $scope.users.length; j++) {
                            if ($scope.users[j].UserData.Id == result[i]) {
                                index = j;
                                break;
                            }
                        }
                        if (index != -1) {
                            $scope.users.splice(index, 1);
                        }
                    }

                    for (var i = 0; i < $scope.users.length; i++) {
                        var dElement = addData($scope.users[i]);
                        data.push(dElement);
                    }

                    $('#table').bootstrapTable('load', data);
                    $('#table').bootstrapTable('resetWidth');
                } else {
                    showNotify("Error", "Error while performing data deletion", "danger");
                    usSpinnerService.stop('spinner-1');
                    $scope.loading = false;

                    $('#table').bootstrapTable('resetWidth');
                }
            },
                function (result) {
                    showNotify("Error", "Error while performing data deletion", "danger");
                    usSpinnerService.stop('spinner-1');
                    $scope.loading = false;
                });
    }



    this.downloadCsv = function (_callback, $http, $scope, url, usSpinnerService, getAll) {
        var responseSuccess = null;
        var responseNoError = null;

        usSpinnerService.spin("spinner-1");

        this.downloadCsvById($http, $scope, url, getAll)
            .then(function (data, status, headers) {
                usSpinnerService.stop('spinner-1');
                responseSuccess = true;
                try {
                    headers = headers();

                    var filename = headers['x-file-name'];
                    var contentType = headers['content-type'];
                    var blob = new Blob([data], { type: contentType });

                    //Check if user is using IE
                    var ua = window.navigator.userAgent;
                    var msie = ua.indexOf("MSIE ");

                    if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./)) {
                        window.navigator.msSaveBlob(blob, filename);
                        responseNoError = true;
                        _callback(responseSuccess, responseNoError);
                    } else // If another browser, return 0
                    {
                        //Create a url to the blob
                        var url = window.URL.createObjectURL(blob);

                        var linkElement = document.createElement('a');
                        linkElement.setAttribute('href', url);
                        linkElement.setAttribute("download", filename);

                        //Force a download
                        var clickEvent = new MouseEvent("click",
                        {
                            "view": window,
                            "bubbles": true,
                            "cancelable": false
                        });
                        linkElement.dispatchEvent(clickEvent);
                        responseNoError = true;
                        _callback(responseSuccess, responseNoError);
                    }

                } catch (ex) {
                    console.log(ex);
                    responseNoError = false;
                    _callback(responseSuccess, responseNoError);
                }
            });
    }

    this.downloadCsvById = function ($http, $scope, url, getAll) {
        var dfd = $.Deferred();

        var parameters = {};
        if (getAll) {
            parameters = {
                userId: $scope.userData.UserData.Id,
                getAll: getAll
            }
        }
        $http({
            method: 'GET',
            url: url,
            params: parameters,
            responseType: 'arraybuffer'
        }).success(function (data, status, headers) {

            return dfd.resolve(data, status, headers);
        });

        return dfd.promise();
    }
});