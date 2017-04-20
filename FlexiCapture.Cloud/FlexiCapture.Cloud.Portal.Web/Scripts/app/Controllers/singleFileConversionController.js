(function () {
    var singleFileConversionController = function ($scope, $http, $timeout, $location, $state, $rootScope, $window, $cookies, usSpinnerService, Idle, Keepalive, $uibModal, manageFilesHttpService, manageUserProfileHttpService) {

        $scope.profiles = []
        $scope.changeCount = 0;
        $scope.currentProfile = {};
        var profilesUrl = $$ApiUrl + "/userProfile";
        var customProfileUrl = $$ApiUrl + "/customProfile";
        $scope.NewProfileName = "";
        $scope.newProfile = false;
        $scope.profileIsChanged = false;
        $scope.defaultProfileId = -1;
        $scope.oldDefaultProfileId = -1;

        $scope.showNewProfile = function (show) {

            $scope.newProfile = show;
            $scope.NewProfileName = "";

        }

        var singleProfile = function () {
            $scope.loadData = true;
            $scope.languages = [];
            $scope.selectedLanguages = [];
            $scope.showSaveProfileBtn = false;
            $scope.showSaveProfilePanel = false;
            manageUserProfileHttpService.getToUserProfiles($http, $scope, profilesUrl, usSpinnerService, 0);
            // $scope.loadData = false;
            $scope.profileIsChanged = false;
        };
        singleProfile();

        $scope.showFileNamePanel = function () {
            $scope.showSaveProfilePanel = true;
        }

        $scope.showNewProfile = function (show) {

            $scope.newProfile = show;
            $scope.NewProfileName = "";

        }

        $scope.updateSettings = function () {
            //alert(JSON.stringify($scope.profiles));
            //$scope.currentProfile = {};
            var data = [];
            var isEdit = $scope.profileIsChanged;
            var purl = $$ApiUrl + "/userProfile";
            manageUserProfileHttpService.manageProfile($http, $scope, data, purl, usSpinnerService, isEdit);
            $scope.showNewProfile(false);
            $scope.profileIsChanged = false;
        }


        $scope.selectLang = function (id) {
            $scope.showSaveProfilePanel = true;

            for (i = 0; i < $scope.currentProfile.AvailableLanguages.length; i++) {
                if ($scope.currentProfile.AvailableLanguages[i].Id == id) {
                    // alert($scope.currentProfile.AvailableLanguages[i].Selected);

                    if ($scope.currentProfile.AvailableLanguages[i].Selected) {
                        var selL = {};
                        selL.Id = $scope.currentProfile.AvailableLanguages[i].Id;
                        selL.Name = $scope.currentProfile.AvailableLanguages[i].Name;

                        $scope.currentProfile.SelectedLanguages.push(selL);
                        console.log(JSON.stringify($scope.currentProfile.SelectedLanguages));
                    } else {
                        for (var k = 0; k < $scope.currentProfile.SelectedLanguages.length; k++) {
                            if ($scope.currentProfile.SelectedLanguages[k].Id == id) {
                                $scope.currentProfile.SelectedLanguages.splice(k, 1);
                                break;
                            }
                        }
                        console.log(JSON.stringify($scope.currentProfile.SelectedLanguages));

                    }
                    $scope.selNames = "";
                    var cLang = 0;
                    for (var k = 0; k < $scope.currentProfile.SelectedLanguages.length; k++) {
                        $scope.selNames += $scope.currentProfile.SelectedLanguages[k].Name + "; ";
                        cLang++;
                        if (cLang > 2) {
                            $scope.selNames += "...";
                            break;
                        }
                    }
                }
            }

        };

        $scope.hideNewProfilePanel = function () {
            $scope.showSaveProfilePanel = false;
            $scope.newProfile = false;
        }

        $scope.addCustomProfile = function () {
            if ($scope.NewProfileName == "") return;
            $scope.customProfile = $scope.currentProfile;
            $scope.customProfile.Name = $scope.NewProfileName;
            var data = [];
            manageUserProfileHttpService.addCustomProfile($http, $scope, data, customProfileUrl, usSpinnerService);
            $scope.hideNewProfilePanel();

        }

        $scope.selectFormat = function (id) {
            $scope.showSaveProfilePanel = true;

            for (i = 0; i < $scope.currentProfile.AvailableExportFormats.length; i++) {
                if ($scope.currentProfile.AvailableExportFormats[i].Id == id) {
                    // alert($scope.currentProfile.AvailableLanguages[i].Selected);

                    if ($scope.currentProfile.AvailableExportFormats[i].Selected) {
                        var selL = {};
                        selL.Id = $scope.currentProfile.AvailableExportFormats[i].Id;
                        selL.Name = $scope.currentProfile.AvailableExportFormats[i].Name;

                        $scope.currentProfile.SelectedExportFormats.push(selL);
                        console.log(JSON.stringify($scope.currentProfile.SelectedExportFormats));
                    } else {
                        for (var k = 0; k < $scope.currentProfile.SelectedExportFormats.length; k++) {
                            if ($scope.currentProfile.SelectedExportFormats[k].Id == id) {
                                $scope.currentProfile.SelectedExportFormats.splice(k, 1);
                                break;
                            }
                        }
                        console.log(JSON.stringify($scope.currentProfile.SelectedExportFormats));

                    }
                    $scope.selFormats = "";
                    var cFormat = 0;
                    for (var k = 0; k < $scope.currentProfile.SelectedExportFormats.length; k++) {
                        $scope.selFormats += $scope.currentProfile.SelectedExportFormats[k].Name + "; ";
                        cFormat++;
                        if (cFormat > 2) {
                            $scope.selFormats += "...";
                            break;
                        }
                    }
                }
            }

        };

        $scope.changeProfile = function () {

            $scope.showSaveProfilePanel = false;

            for (var i = 0; i < $scope.profiles.length; i++) {
                if ($scope.currentProfile.Id == $scope.profiles[i].Id) {
                    $scope.currentProfile = $scope.profiles[i];
                    $scope.defaultProfileId = $scope.currentProfile.Id;
                    console.log("Up");
                    break;
                }
            }
            $scope.selNames = "";
            var cLang = 0;
            var cFormat = 0;
            for (var k = 0; k < $scope.currentProfile.SelectedLanguages.length; k++) {
                $scope.selNames += $scope.currentProfile.SelectedLanguages[k].Name + "; ";
                cLang++;
                if (cLang > 2) {
                    $scope.selNames += "...";
                    break;
                }
            }
            $scope.selFormats = "";
            for (var k = 0; k < $scope.currentProfile.SelectedExportFormats.length; k++) {
                $scope.selFormats += $scope.currentProfile.SelectedExportFormats[k].Name + "; ";
                cFormat++;
                if (cFormat > 2) {
                    $scope.selFormats += "...";
                    break;
                }
            }

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

            $scope.changeCount = 0;
            $scope.profileIsChanged = false;

        };

        $scope.files = [];
        var url = $$ApiUrl + "/documents";
        var singleFileConversion = function () {


            // var dropZone = document.getElementById('drop-zone');
            var uploadForm = document.getElementById('tformdiv');
            var uploadForm3 = document.getElementsByTagName('form');

            $scope.uploadImages = function () {

                //alert(JSON.stringify($scope.currentProfile));
                if ($scope.files.length > 0) {
                    $scope.showProgressBar = true;
                    usSpinnerService.spin('spinner-1');
                    var data = new FormData();


                    data.append("uploadedFile0", $scope.files[0]);
                    data.append("serviceId", $scope.serviceStateId);
                    data.append("userId", $scope.userData.UserData.Id);
                    data.append("profile", JSON.stringify($scope.currentProfile));

                    var fUrl = url;
                    manageFilesHttpService.uploadFiles($http, $scope, $state, fUrl, usSpinnerService, data);
                } else {
                    BootstrapDialog.alert({
                        title: 'Warning',
                        message: 'Not selected files!',
                        type: BootstrapDialog.TYPE_WARNING,
                        closable: true
                    });
                }
            }

            $scope.addThruChoice = function (element) {
                $scope.addToScopeFiles(element.files);
            }

            $scope.removeFromFiles = function (index) {
                $scope.files.splice(index, 1);
            }

            $scope.addToScopeFiles = function (files) {
                $scope.showProgressBar = false;
                document.getElementById("proBar").style.width = "0%";
                // var uploadFiles = files;
                // for (var i = 0; i < uploadFiles.length; i++) {
                //     var found = false;
                //     for (var j = 0; j < $scope.files.length; j++) {
                //         if ($scope.files[j].name == uploadFiles[i].name) {
                //             found = true;
                //         }
                //     }
                //     if (!found) { $scope.files.push(uploadFiles[i]); }
                // }


                $scope.files = [];
                $scope.files.push(files[0]);

                $scope.$apply();
            }
            // dropZone.ondrop = function (e) {
            //     e.preventDefault();
            //     this.className = 'upload-drop-zone';
            //     $scope.addToScopeFiles(e.dataTransfer.files);
            // }

            // dropZone.ondragover = function () {
            //     this.className = 'upload-drop-zone drop';
            //     return false;
            // }

            // dropZone.ondragleave = function () {
            //     this.className = 'upload-drop-zone';
            //     return false;
            // }

        };
        singleFileConversion();


        $scope.changeProfile = function () {


            for (var i = 0; i < $scope.profiles.length; i++) {
                if ($scope.currentProfile.Id == $scope.profiles[i].Id) {
                    $scope.currentProfile = $scope.profiles[i];
                    $scope.defaultProfileId = $scope.currentProfile.Id;
                    console.log("Up");
                    break;
                }
            }


            $scope.changeCount = 0;
            $scope.profileIsChanged = false;

        };

        $scope.$watch('$viewContentLoaded',
            function () {
                $timeout(function () {
                    //do something
                    $scope.changeCount = 0;
                    $scope.profileIsChanged = false;
                }, 0);
            });
        $scope.$watchCollection('currentProfile', function () {

            if ($scope.changeCount > 0)
                $scope.profileIsChanged = true;
            $scope.changeCount++;

        });

        $scope.imChanged = function () {
            if ($scope.changeCount > 0)
                $scope.profileIsChanged = true;
            $scope.changeCount++;

        }
    };


    fccApp.controller("singleFileConversionController", ["$scope", "$http", "$timeout", "$location", "$state", "$rootScope", "$window", "$cookies", "usSpinnerService", "Idle", "Keepalive", "$uibModal", "manageFilesHttpService", "manageUserProfileHttpService", singleFileConversionController]);
}())