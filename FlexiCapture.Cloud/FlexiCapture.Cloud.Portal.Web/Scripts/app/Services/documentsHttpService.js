fccApp.service('documentsHttpService', function () {
    //добавляем данные user в для таблицы
    function addData(document) {
        var dElement = {};
        dElement.Id = document.Id;
        dElement.ViewId = 12010000 + document.Id;
        dElement.taskId = document.TaskId;
        dElement.ViewTaskId = 12010000 + parseInt(document.TaskId);
        dElement.dateTime = document.DateTime;
        dElement.fileSize = document.FileSize;
        dElement.fileName = document.OriginalFileName;
        dElement.stateName = (document.StateId == 2 || document.StateId == 1 ? "<i class='fa fa-spinner fa-pulse fa-3x fa-fw'></i>" : "") + document.StateName;
        dElement.typeName = document.TypeName;

        switch (document.ServiceId) {
            case 1:
                dElement.serviceName = "Single";
                break;
            case 2:
                dElement.serviceName = "Batch";
                break;
            case 3:
                dElement.serviceName = "FTP";
                break;
            case 4:
                dElement.serviceName = "Email";
                break;
            case 5:
                dElement.serviceName = "WebOCRApi";
                break;
        }






        var link = "";
        for (var i = 0; i < document.ResultDocuments.length; i++) {
            var doc = document.ResultDocuments[i];

            var type = doc.TypeName;
            var origFilename = doc.OriginalFileName;
            var url = doc.Url;
            link += "<p><a id='" + doc.Id + "' href='javascript: void(0);' class='download-link'>" + ((doc.TypeName == "ZIP") ? "Results (Zip)" : doc.TypeName) +
                " <i class='fa fa-download' aria-hidden='true'></i></a>" +
                "<a class='download-link preview' style='margin-left: 25px;' id='" + doc.Id + "' href='javascript: void(0);'><i class='fa fa-sticky-note-o'></i></a>"
                + "</p>";//"<p><a href='" + doc.Url + "'> <i class='fa fa-download' aria-hidden='true'></i>" + doc.OriginalFileName + "</a></p>"

        }

        if (document.DocumentErrors != null) {
            for (var k in document.DocumentErrors) {
                link += "<p><a id='" +
                    document.Id +
                    "' style='color: red;' class='result-link'> <i class='fa fa-download' aria-hidden='true'></i>  " +
                    document.DocumentErrors[k].DocumentName +
                    "</a></p>";
            }

        }


        dElement.results = link;

        return dElement;
    }

    this.alerter = function (id) {
        alert(id);

    }
    this.downloadDocument = function (docId) {
        this.downloadDocumentById($http, $scope, docId, $$ApiUrl + "/downloadfile")
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
                        var clickEvent = new MouseEvent("click",
                        {
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

    this.downloadDocumentById = function ($http, $scope, docId, url) {
        var dfd = $.Deferred();

        $http({
            method: 'GET',
            url: url + "/" + docId,
            responseType: 'arraybuffer'
        }).success(function (data, status, headers) {

            return dfd.resolve(data, status, headers);


            //$http.get(url, {
            //    params: { documentId: docId }
            //}).then(function(response) {
            //    return dfd.resolve(response.data);
            //});
            //return dfd.promise();
        });

        return dfd.promise();
    }


    //get to clients list
    this.getToDocuments = function ($http, $scope, $state, data, url, usSpinnerService) {
        $scope.documents = [];
        $scope.loading = true;
        usSpinnerService.spin("spinner-1");

        var dfd = $.Deferred();

        $http.get(url, {
            params: { userId: $scope.userData.UserData.Id, serviceId: $scope.serviceStateId }
        }).then(function (response) {
            var docs = []
            docs = JSON.parse(response.data);
            for (var i = 0; i < docs.length; i++) {
                var document = {};
                document = docs[i];
                $scope.documents.push(document);
                // $scope.loading = true;
                var dElement = addData(document);
                data.push(dElement);
            }


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
            return dfd.resolve($scope.documents);
        });

        return dfd.promise();
    }


    this.getToDocumentsSilent = function ($http, $scope, $state, data, url, usSpinnerService) {
        $scope.documents = [];
        $http.get(url, {
            params: { userId: $scope.userData.UserData.Id, serviceId: $scope.serviceStateId }
        }).then(function (response) {
            var docs = [];
            data = [];
            docs = JSON.parse(response.data);
            for (var i = 0; i < docs.length; i++) {
                var document = {};
                document = docs[i];
                $scope.documents.push(document);
                // $scope.loading = true;
                var dElement = addData(document);
                data.push(dElement);
            }
            $('#table').bootstrapTable('load', data);
        });
    }


    // delete position
    this.deleteSelectedPositions = function ($http, $scope, data, deleteData, url, usSpinnerService) {
        usSpinnerService.spin("spinner-1");
        var methodType = "DELETE";
        $http({
            url: url,
            method: methodType,
            headers: {
                'Content-Type': 'application/json'
            },
            data: JSON.stringify(deleteData)
        })
            .then(function (response) {
                $scope.loadData = false;
                if (response.data == "Success") {

                    data = [];

                    for (var i = 0; i < deleteData.length; i++) {
                        for (var j = 0; j < $scope.documents.length; j++) {
                            if (deleteData[i].Id == $scope.documents[j].Id) {
                                $scope.documents.splice(j, 1);
                            }
                        }
                    }

                    for (var i = 0; i < $scope.documents.length; i++) {
                        var dElement = addData($scope.documents[i]);
                        data.push(dElement);
                    }

                    $('#table').bootstrapTable('load', data);
                    showNotify("Success", "Documents have been deleted successfully!", "success");
                    // success
                } else {
                    showNotify("Fail", "Failed to delete documents", "danger");
                }
                usSpinnerService.stop('spinner-1');
                $scope.loading = false;
                $('#table').bootstrapTable('resetWidth');
            },
                function (response) { // optional
                    // failed
                    $scope.loading = false;
                    usSpinnerService.stop('spinner-1');
                    showNotify("Fail", "Failed to delete documents", "danger");
                    $('#table').bootstrapTable('resetWidth');
                });
        $('#table').bootstrapTable('resetWidth');
    }


});