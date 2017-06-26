fccApp.service('subscriptionsPlansHttpService', function () {




    plansList = [
        {
            id: 1,
            name: "First",
            autoRenewal: 1, // one time
            planExpiration: 60,
            cost: 0,
            pagesInInterval: 5000
        },
        {
            id: 2,
            name: "Second",
            autoRenewal: 2, // monthly
            planExpiration: 60,
            cost: 1,
            pagesInInterval: 5000
        },
        {
            id: 3,
            name: "Third",
            autoRenewal: 3, // annual
            planExpiration: 60,
            cost: 2,
            pagesInInterval: 5000
        }
    ];



    //get to plans list
    this.getToPlans = function ($http, $scope, $state, data, url, usSpinnerService) {
        $scope.plans = [];
        for (var k in plansList) {
            var dElement = {};

            //{
            //    name: "First",
            //    autoRenewal: 1, // one time
            //    planExpiration: 60,
            //    cost: 0,
            //    pagesInInterval: 5000
            //}

            //<th data-checkbox="true" data-click-to-select="true"></th>
            //<th data-field="name" data-visible="false" data-sortable="true">Name</th>
            //<th data-field="autoRenewal" data-visible="false" data-sortable="true">Auto renewal</th>
            //<th data-field="expiration" data-sortable="true">Expiration</th>
            //<th data-field="cost" data-sortable="true">Cost (per page)</th>
            //<th data-field="pagesPerInterval" data-sortable="true">Pages per renewal interval</th>

            dElement.id = plansList[k].id;
            dElement.name = plansList[k].name;

            switch (plansList[k].autoRenewal) {
                case 1:
                    dElement.autoRenewal = "One time purchase";
                    break;
                case 2:
                    dElement.autoRenewal = "Monthly";
                    break;
                case 3:
                    dElement.autoRenewal = "Annual";
                    break;
            }

            dElement.expiration = plansList[k].expiration;
            dElement.cost = plansList[k].cost;
            dElement.pagesPerInterval = plansList[k].pagesInInterval;

            $scope.plans.push(dElement);
        }

        $('#table').bootstrapTable({
            data: $scope.plans,
            height: '100%',
            onPostBody: function () {
                $('#table').bootstrapTable('resetView');
            }

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