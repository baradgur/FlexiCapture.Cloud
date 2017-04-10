fccApp.service('documentsHttpService', function () {
    //добавляем данные user в для таблицы
    function addData(document) {
        var dElement = {};
        dElement.Id = document.Id;
        dElement.taskId = document.TaskId;
        dElement.dateTime =document.DateTime;
        dElement.fileSize = document.FileSize;
        dElement.fileName = document.OriginalFileName;
        dElement.stateName = document.StateName;
        dElement.typeName = document.TypeName;
        dElement.href ="<a href ='"+ document.Url+"'  download='"+document.OriginalFileName+"'>link</a>";
        return dElement;
    }


    //get to clients list
    this.getToDocuments = function ($http, $scope, $state, data, url, usSpinnerService) {
        $scope.documents = [];
        usSpinnerService.spin("spinner-1");

        $http.get(url, {
            params: { userId: $scope.userData.UserData.Id, serviceId: $scope.serviceStateId }
        }).then(function (response) {
            var docs =[]
            docs = JSON.parse(response.data);
            for (i = 0; i < docs.length; i++) {
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


});