fccApp.service('manageUserProfileHttpService', function () {

    //get to clients list
    this.getToUserProfiles = function ($http, $scope, $state, data, url, usSpinnerService) {
        $scope.profiles = []
        $scope.currentProfile = {};
        usSpinnerService.spin("spinner-1");


        $http.get(url, {
            params: { userId: $scope.userData.UserData.Id }
        }).then(function (response) {


            $scope.profiles = JSON.parse(response.data);



            if ($scope.profiles.length > 0) $scope.currentProfile = $scope.profiles[0];

            $scope.changeCount =0;
            usSpinnerService.stop('spinner-1');
            $scope.loadData = false;
            window.scope = $scope;
            $scope.profileIsChanged = false;
            $scope.changeCount =0;
        });
    }

this.manageProfile = function ($http, $scope, data, url, usSpinnerService, isEdit) {


        usSpinnerService.spin("spinner-1");
        var methodType = "POST";
        if (isEdit) {
            methodType = "PUT";
        }

        $http({
            url: url,
            method: methodType,
            contentType: "application/json",
            data: JSON.stringify($scope.currentProfile)
        })
            .then(function (response) {
                usSpinnerService.stop('spinner-1');
                $scope.loadData = false;
                console.log(JSON.stringify(response.data));
                //var user = JSON.parse(response.data);
                 
                   
                    
                    var notificationT = "added";
                    if (isEdit) notificationT = "updated";

                    showNotify("Успех", "Profile " + $scope.currentProfile.Name + " was successfully " + notificationT, "success");
                                        // success
                
            },
            function (response) { // optional
                // failed
                usSpinnerService.stop('spinner-1');
                showNotify("Error", "Error in profileupdate", "danger");
               
            });
        $('#table').bootstrapTable('resetWidth');
    }
});