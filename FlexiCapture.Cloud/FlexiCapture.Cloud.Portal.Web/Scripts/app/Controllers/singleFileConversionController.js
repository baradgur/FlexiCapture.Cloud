(function () {
    var singleFileConversionController = function ($scope, $http, $location, $state, $rootScope, $window, $cookies, usSpinnerService, Idle, Keepalive, $uibModal,manageFilesHttpService) {
        $scope.files = [];
        var url = $$ApiUrl + "/documents";
        var singleFileConversion = function () {

            var dropZone = document.getElementById('drop-zone');
            var uploadForm = document.getElementById('tformdiv');
            var uploadForm3 = document.getElementsByTagName('form');

            $scope.uploadImages = function () {
                if ($scope.files.length > 0) {
                    $scope.showProgressBar = true;
                    usSpinnerService.spin('spinner-1');
                    var data = new FormData();

                    //for (var i = 0; i < $scope.files.length; i++) {
                        
                        data.append("uploadedFile0" , $scope.files[0]);
                        data.append("serviceId",2);
                        data.append("userId",2);
                   // }
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
            dropZone.ondrop = function (e) {
                e.preventDefault();
                this.className = 'upload-drop-zone';
                $scope.addToScopeFiles(e.dataTransfer.files);
            }

            dropZone.ondragover = function () {
                this.className = 'upload-drop-zone drop';
                return false;
            }

            dropZone.ondragleave = function () {
                this.className = 'upload-drop-zone';
                return false;
            }

        };
        singleFileConversion();

    };


    fccApp.controller("singleFileConversionController", ["$scope", "$http", "$location", "$state", "$rootScope", "$window", "$cookies", "usSpinnerService", "Idle", "Keepalive", "$uibModal","manageFilesHttpService", singleFileConversionController]);
}())