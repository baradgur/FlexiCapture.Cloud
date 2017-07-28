fccApp.service('usersHttpService', function() {
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
    this.getToUsersList = function($http, $scope, $state, data, url, usSpinnerService) {
        $scope.users = [];
        usSpinnerService.spin("spinner-1");

        $.ajax({
            url: url,
            type: 'GET',
            dataType: 'json',
            data: { model: $scope.userData.UserData },
            success: function(response) {
                //$scope.userData.UserData
                for (i = 0; i < response.UsersData.length; i++) {
                    var user = {};

                    user = response.UsersData[i];

                    $scope.users.push(user);

                    $scope.loading = true;
                    var dElement = addData(user);
                    data.push(dElement);
                }
                $scope.availableRoles = response.UserRolesData;
                $scope.availableCompanies = response.CompaniesData;
                $scope.availableCompanyUnits = response.CompanyUnitsData;
                $scope.availableLoginStates = response.LoginStatesData;

                $('#table').bootstrapTable({
                    data: data,
                    height: '100%',
                    onPostBody: function() {
                        $('#table').bootstrapTable('resetView');
                        usSpinnerService.stop('spinner-1');
                    }

                });

                $('[data-toggle="tooltip"]').tooltip()

                var $result = $('#eventsResult');

                $('#table').on('all.bs.table',
                        function(e, name, args) {
                            // console.log('Event:', name, ', data:', args);
                        })
                    .on('click-row.bs.table',
                        function(e, row, $element) {
                            // $result.text('Event: click-row.bs.table'+ JSON.stringify(row.userName));
                        });

                $('#table').bootstrapTable('resetWidth');



                $scope.loading = false;
                window.scope = $scope;
            }
        });
    }

    //add or Edit user
    this.manageUser = function(callback, $http, $scope, data, url, usSpinnerService, isEdit) {
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
            .then(function(response) {
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
                function(response) { // optional
                    // failed
                    usSpinnerService.stop('spinner-1');
                    showNotify("Успех", "Error occurred while adding user", "danger");
                    $('#table').bootstrapTable('resetWidth');
                    callback(responseSuccess, responseNoError);
                });
        $('#table').bootstrapTable('resetWidth');

    }

    //get to clients list
    this.getToUserProfile = function($http, $scope, $state, url, usSpinnerService) {
        $scope.user = {};
        usSpinnerService.spin("spinner-1");

        $http.get(url, {
            params: { userId: $scope.userData.UserData.Id }
        })

        .then(function(response) {
            // alert(JSON.stringify(response.data));
            $scope.currentUser = JSON.parse(response.data);
            $scope.user = angular.copy($scope.currentUser);
            usSpinnerService.stop('spinner-1');
            $scope.loading = false;
            window.scope = $scope;
        });
    }

    //edit user profile
    this.updateUserProfile = function($http, $scope, url, usSpinnerService) {


        usSpinnerService.spin("spinner-1");
        var methodType = "PUT";

        $http({
                url: url,
                method: methodType,
                contentType: "application/json",
                data: JSON.stringify($scope.currentUser)
            })
            .then(function(response) {
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
                function(response) { // optional
                    // failed
                    usSpinnerService.stop('spinner-1');
                    showNotify("Успех", "Unable to update profile", "danger");
                });
    };

    //delete by ID
    this.deleteUser = function($http, $scope, usSpinnerService, url, rParams) {


        usSpinnerService.spin("spinner-1");

        $http.delete(url, {
                params: rParams
            })
            .success(function(result) {
                    if (result == "OK") {
                        usSpinnerService.stop('spinner-1');
                        showNotify("Success", "Data deleted successfully", "success");

                        $scope.loading = false;
                        return result;
                    } else {
                        showNotify("Error", "Error while performing data deletion", "danger");
                        usSpinnerService.stop('spinner-1');
                        $scope.loading = false;
                    }
                },
                function(result) {
                    showNotify("Error", "Error while performing data deletion", "danger");
                    usSpinnerService.stop('spinner-1');
                    $scope.loading = false;
                });
    }


});