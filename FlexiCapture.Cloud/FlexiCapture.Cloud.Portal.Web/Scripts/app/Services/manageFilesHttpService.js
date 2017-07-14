fccApp.service('manageFilesHttpService', function () {


    // adding images to the dish gallery
    this.uploadFiles = function ($http, $scope, $state, fUrl, usSpinnerService, data) {

        var objXhr = new XMLHttpRequest();
        objXhr.onreadystatechange = stateChange;
        objXhr.upload.onprogress = updateProgress;


        // SEND FILE DETAILS TO THE API.
        objXhr.open("POST", fUrl);
        objXhr.send(data);

        //statechange listener
        function stateChange() {
            if (objXhr.readyState == 4) {// 4 = "DONE"
                if (objXhr.status == 200) {// 200 = OK
                    if (JSON.parse(objXhr.response) != null) {
                        // $scope.currentDish.ImagesProperties = JSON.parse(objXhr.response);

                        showNotify("Successfull", "Loading was successful.", "success");

                        switch ($scope.serviceStateId) {
                            case 1:
                                $state.go("main.dashboard.library");
                                break;

                            case 2:
                                $state.go("main.dashboard.library");
                                break;

                            default: break;
                        }

                    }
                    else { showNotify("Error", "Error loading files", "danger"); }
                }
                else {
                    showNotify("Error", "Server error. Contact your administrator. Error code: " +
                            objXhr.status + " " + objXhr.statusText, "danger");
                }
                $scope.files = [];
                $scope.$apply();
                usSpinnerService.stop('spinner-1');
            }
        }
        function updateProgress(evt) {
            if (evt.lengthComputable) {
                var percentComplete = (evt.loaded / evt.total) * 100;
                document.getElementById("proBar").style.width = percentComplete + "%";
            }
            else { console.log('unable to compute'); }
        }

    }


});


