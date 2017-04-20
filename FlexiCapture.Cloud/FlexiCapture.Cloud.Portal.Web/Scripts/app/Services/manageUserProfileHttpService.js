fccApp.service('manageUserProfileHttpService', function () {
    var ms = this;
    //get to clients list
    this.getToUserProfiles = function ($http, $scope, url, usSpinnerService, index) {


        usSpinnerService.spin("spinner-1");


        $http.get(url, {
            params: { userId: $scope.userData.UserData.Id, serviceId: $scope.serviceStateId }
        }).then(function (response) {

            // alert(response.data);
            var tt = JSON.parse(response.data);
            $scope.profiles = JSON.parse(response.data);

            if ($scope.profiles.length > 0) {
                for (var i = 0; i < $scope.profiles.length; i++) {
                    if ($scope.profiles[i].DefaultServiceId == $scope.serviceStateId) {
                        $scope.profiles[i].isDefault = true;
                        $scope.defaultProfileId = $scope.profiles[i].Id;
                        $scope.oldDefaultProfileId = $scope.defaultProfileId;
                        $scope.currentProfile = $scope.profiles[i];


                        for (var j = 0; j < $scope.currentProfile.AvailableLanguages.length; j++) {
                            for (var k = 0; k < $scope.currentProfile.SelectedLanguages.length; k++) {
                                if ($scope.currentProfile.SelectedLanguages[k].Id == $scope.currentProfile.AvailableLanguages[j].Id)
                                    $scope.currentProfile.AvailableLanguages[j].Selected = true;
                            }
                        }

                        for (var j = 0; j < $scope.currentProfile.AvailableExportFormats.length; j++) {
                            for (var k = 0; k < $scope.currentProfile.SelectedExportFormats.length; k++) {
                                if ($scope.currentProfile.SelectedExportFormats[k].Id == $scope.currentProfile.AvailableExportFormats[j].Id)
                                    $scope.currentProfile.AvailableExportFormats[j].Selected = true;
                            }
                        }


                        //$scope.selectedLanguages = [$scope.languages[0]];
                    } else {
                        $scope.profiles[i].isDefault = false;
                    }
                }

            };
            $scope.selNames = "";
            $scope.selFormats = "";
            var cLang = 0;
            var cFormat = 0;

            for (var i = 0; i < $scope.currentProfile.SelectedExportFormats.length; i++) {
                $scope.selFormats += $scope.currentProfile.SelectedExportFormats[i].Name + ";";
                cFormat++;
                if (cFormat > 2) {
                    $scope.selFormats += "...";
                    break;
                }
            }
            for (var i = 0; i < $scope.currentProfile.SelectedLanguages.length; i++) {
                $scope.selNames += $scope.currentProfile.SelectedLanguages[i].Name + ";";
                cLang++;
                if (cLang > 2) {
                    $scope.selNames += "...";
                    break;
                }
            }

            $scope.changeCount = 0;
            usSpinnerService.stop('spinner-1');
            $scope.loadData = false;
            window.scope = $scope;
            $scope.profileIsChanged = false;
            $scope.changeCount = 0;
        });
    }

    this.manageProfile = function ($http, $scope, data, url, usSpinnerService, isEdit) {


        usSpinnerService.spin("spinner-1");
        var methodType = "POST";

        var newData = {};
        newData.UserId = $scope.userData.UserData.Id;
        newData.ProfileName = $scope.NewProfileName;

        if (isEdit) {
            methodType = "PUT";
            newData = {};
            newData = JSON.stringify($scope.currentProfile);
        }
        $scope.currentProfile = {};
        $http({
            url: url,
            method: methodType,
            contentType: "application/json",
            data: newData
        })
            .then(function (response) {
                usSpinnerService.stop('spinner-1');
                $scope.loadData = false;

                var rObj = JSON.parse(response.data);
                $scope.currentProfile = rObj;
                if (methodType == "PUT") {
                    for (var i = 0; i < $scope.profiles.length; i++) {
                        if ($scope.profiles[i].Id == rObj.Id) {
                            $scope.profiles[i] = {};
                            $scope.profiles[i] = rObj;
                            //console.log(JSON.stringify($scope.profiles[i]));
                            $scope.currentProfile = $scope.profiles[i];
                            break;
                        }
                    }

                }

                //profile = rObj;
                if (methodType == "POST") {
                    $scope.profiles.push(rObj);
                };


                var notificationT = "added";
                if (isEdit) notificationT = "updated";

                showNotify("Success", "Profile " + $scope.currentProfile.Name + " was successfully " + notificationT, "success");
                // success

                $scope.profileIsChanged = false;
                $scope.changeCount = 0;


            },
            function (response) { // optional
                // failed
                usSpinnerService.stop('spinner-1');
                showNotify("Error", "Error in profileupdate", "danger");
                $scope.profileIsChanged = false;
                $scope.changeCount = 0;

            });
    }

    this.updateDefaultProfile = function ($http, $scope, url, usSpinnerService) {
        $http.get(url, {
            params: { profileId: $scope.oldDefaultProfileId, newProfileId: $scope.currentProfile.Id, serviceId: $scope.serviceStateId }
        }).then(function (response) {
            var notificationT = "updated";
            showNotify("Success", "Profile " + $scope.currentProfile.Name + " was successfully " + notificationT, "success");
            $scope.profileIsChanged = false;
            $scope.changeCount = 0;
            $scope.oldDefaultProfileId = $scope.defaultProfileId;
        },
            function (response) { // optional
                // failed
                usSpinnerService.stop('spinner-1');
                showNotify("Error", "Error in profile update", "danger");
                $scope.profileIsChanged = false;
                $scope.changeCount = 0;

            });
    }

    this.addCustomProfile = function ($http, $scope, url, usSpinnerService) {


        usSpinnerService.spin("spinner-1");
        var methodType = "POST";

        $scope.newData = {};
        $scope.newData = JSON.stringify($scope.customProfile);
        $scope.customProfile = {};

        $http({
            url: url,
            method: methodType,
            contentType: "application/json",
            data: $scope.newData
        })
            .then(function (response) {
                $scope.newData ={};
                usSpinnerService.stop('spinner-1');
                $scope.loadData = false;

                var rObj = JSON.parse(response.data);
                $scope.profiles.push(rObj);
                for (var i = 0; i < $scope.profiles.length; i++) {
                    if ($scope.profiles[i].Id == rObj.Id) {
                        //$scope.currentProfile = {};
                        $scope.currentProfile = $scope.profiles[i];
                        break;
                    }
                }



                var notificationT = "added";
                showNotify("Success", "Profile " + $scope.currentProfile.Name + " was successfully " + notificationT, "success");
                // success

                $scope.profileIsChanged = false;
                $scope.changeCount = 0;


            },
            function (response) { // optional
                // failed
                usSpinnerService.stop('spinner-1');
                showNotify("Error", "Error in profileupdate", "danger");
                $scope.profileIsChanged = false;
                $scope.changeCount = 0;

            });
    }
});