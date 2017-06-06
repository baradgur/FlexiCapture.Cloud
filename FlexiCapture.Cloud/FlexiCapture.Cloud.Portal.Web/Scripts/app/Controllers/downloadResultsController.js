var downloadResultsController = function ($scope, $http, $location, $state, $uibModal, $uibModalInstance, $stateParams, usSpinnerService, documentsHttpService, items) {
    var vm = this;

    function loadDownloadData() {
        vm.$scope = $scope;
        vm.items = items;
        
        vm.cancel = $uibModalInstance.dismiss;
        vm.downloadDoc = function(doc) {
            documentsHttpService.downloadDocumentById($http, $scope, doc.Id, $$ApiUrl + "/downloadfile")
                    .then(function (data, status, headers) {
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
                            } else // If another browser, return 0
                            {
                                //Create a url to the blob
                                var url = window.URL.createObjectURL(blob);
                                var linkElement = document.createElement('a');
                                linkElement.setAttribute('href', url);
                                linkElement.setAttribute("download", filename);

                                //Force a download
                                var clickEvent = new MouseEvent("click", {
                                    "view": window,
                                    "bubbles": true,
                                    "cancelable": false
                                });
                                linkElement.dispatchEvent(clickEvent);
                            }

                        } catch (ex) {
                            console.log(ex);
                        }
                    });
        }
    }
    loadDownloadData();


};
fccApp.controller("downloadResultsController", ["$scope", "$http", "$location", "$state", "$uibModal", "$uibModalInstance", "$stateParams", "usSpinnerService", "documentsHttpService", downloadResultsController]);