fccApp.service('documentsHttpService', function() {
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
        dElement.stateName = (document.StateId == 2 ? "<i class='fa fa-spinner fa-pulse fa-3x fa-fw'></i>" : "") + document.StateName;
        dElement.typeName = document.TypeName;


        var link = "";
        for (var i = 0; i < document.ResultDocuments.length; i++) {
            var doc = document.ResultDocuments[i];

            var type = doc.TypeName;
            var origFilename = doc.OriginalFileName;
            var url = doc.Url;
            link += "<p><a ng-click='documentsHttpService.alerter(" + doc.Id + ")'> <i class='fa fa-download' aria-hidden='true'></i>" + type + "</a></p>"

        }
        dElement.results = "Results (" + document.ResultDocuments.length + ")";

        return dElement;
    }

    this.alerter = function(id) {
        alert(id);

    }

    this.downloadDocumentById = function($http, $scope, docId, url) {
        var dfd = $.Deferred();

        $http({
            method: 'GET',
            url: url,
            responseType: 'arraybuffer',
            params: { documentId: docId }
        }).success(function(data, status, headers) {

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
    this.getToDocuments = function($http, $scope, $state, data, url, usSpinnerService) {
        $scope.documents = [];
        $scope.loading = true;
        usSpinnerService.spin("spinner-1");

        $http.get(url, {
            params: { userId: $scope.userData.UserData.Id, serviceId: $scope.serviceStateId }
        }).then(function(response) {
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
                onPostBody: function() {
                    $('#table').bootstrapTable('resetView');
                }

            });

            $('[data-toggle="tooltip"]').tooltip()

            var $result = $('#eventsResult');

            $('#table').on('all.bs.table', function(e, name, args) {
                    // console.log('Event:', name, ', data:', args);
                })
                .on('click-row.bs.table', function(e, row, $element) {
                    // $result.text('Event: click-row.bs.table'+ JSON.stringify(row.userName));
                })

            $('#table').bootstrapTable('resetWidth');
            usSpinnerService.stop('spinner-1');
            $scope.loading = false;
            window.scope = $scope;
        });
    }


    this.getToDocumentsSilent = function($http, $scope, $state, data, url, usSpinnerService) {
        $scope.documents = [];
        $http.get(url, {
            params: { userId: $scope.userData.UserData.Id, serviceId: $scope.serviceStateId }
        }).then(function(response) {
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
    this.deleteSelectedPositions = function($http, $scope, data, deleteData, url, usSpinnerService) {
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
            .then(function(response) {
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
                function(response) { // optional
                    // failed
                    $scope.loading = false;
                    usSpinnerService.stop('spinner-1');
                    showNotify("Fail", "Failed to delete documents", "danger");
                    $('#table').bootstrapTable('resetWidth');
                });
        $('#table').bootstrapTable('resetWidth');
    }


});