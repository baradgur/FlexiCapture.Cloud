fccApp.service('documentsHttpService', function () {
    //добавляем данные user в для таблицы
    function addData(document) {
        var dElement = {};
        dElement.Id = document.Id;
        dElement.taskId = document.TaskId;
        dElement.dateTime = document.DateTime;
        dElement.fileSize = document.FileSize;
        dElement.fileName = document.OriginalFileName;
        dElement.stateName = document.StateName;
        dElement.typeName = document.TypeName;
        dElement.href = "<a href ='" + document.Url + "'  download='" + document.OriginalFileName + "'>Original File</a>";

        var link = "";
        for (i = 0; i < document.ResultDocuments.length; i++) {
            var doc = document.ResultDocuments[i];

            var type = doc.TypeName;
            var origFilename = doc.OriginalFileName;
            var url = doc.Url;
            link += "<p><a href='" + url + "' download='" + origFilename + "'>" + type + "</a></p>"

        }
        dElement.results = link;

        return dElement;
    }


    //get to clients list
    this.getToDocuments = function ($http, $scope, $state, data, url, usSpinnerService) {
        $scope.documents = [];
        usSpinnerService.spin("spinner-1");

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
        });
    }


    this.getToDocumentsSilent = function ($http, $scope, $state, data, url, usSpinnerService) {
        $scope.documents = [];
        $http.get(url, {
            params: { userId: $scope.userData.UserData.Id, serviceId: $scope.serviceStateId }
        }).then(function (response) {
            var docs = [];
            data =[];
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


});