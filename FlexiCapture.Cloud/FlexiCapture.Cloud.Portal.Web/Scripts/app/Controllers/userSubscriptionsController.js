(function () {
    var userSubscriptionsController = function ($scope,
        $http,
        $location,
        $state,
        $rootScope,
        $window,
        $cookies,
        usSpinnerService,
        Idle,
        Keepalive,
        $uibModal,
        $locale,
        subscriptionsPlansHttpService) {

        $scope.plans = [];
        $scope.currentPlan = null;
        var url = $$ApiUrl + "/SubscriptionPlans";
        var planUseUrl = $$ApiUrl + "/SubscriptionPlanUses";
        $scope.monthToYearSwitcher = false;

        $scope.currentYear = new Date().getFullYear();
        $scope.currentMonth = new Date().getMonth() + 1;
        $scope.months = $locale.DATETIME_FORMATS.MONTH;
        $scope.cardInfo = { type: undefined }
        $scope.save = function (data) {
            //if ($scope.cardForm.$valid) {
            //console.log(data); // valid data saving stuff here
            //}
            subscriptionsPlansHttpService.sendInfoToVault($http,
                $scope,
                "https://api.sandbox.paypal.com/v1/vault/credit-cards/",
                usSpinnerService).
            then(function (response) {
                subscriptionsPlansHttpService.putVaultInfoToDb($http, $scope, $$ApiUrl + "/Vault",
                    usSpinnerService, {
                        UserId: userData.UserData.Id,
                        VaultId: response.id,
                        Id: 0
                    }).then(function(response) {
                    subscriptionsPlansHttpService.sendPayment($http,
                        $scope,
                        $$ApiUrl + "/Vault",
                        usSpinnerService,
                        null);
                }); //console.log(response);
            });
        }

        var CREATE_PAYMENT_URL = 'https://my-store.com/paypal/create-payment';
        var EXECUTE_PAYMENT_URL = 'https://my-store.com/paypal/execute-payment';

        paypal.Button.render({

            env: 'sandbox', // Or 'production'

            commit: true, // Show a 'Pay Now' button

            payment: function () {
                return paypal.request.post(CREATE_PAYMENT_URL).then(function (data) {
                    return data.id;
                });
            },

            onAuthorize: function (data) {
                return paypal.request.post(EXECUTE_PAYMENT_URL, {
                    paymentID: data.paymentID,
                    payerID: data.payerID
                }).then(function () {

                    // The payment is complete!
                    // You can now show a confirmation message to the customer
                });
            }

        }, '#paypal-button');

        $scope.userData = JSON.parse($window.sessionStorage.getItem("UserData"));

        $scope.loadPlans = function () {

            $scope.loading = true;
            usSpinnerService.spin("spinner-1");
            subscriptionsPlansHttpService.getToPlan($http, $scope, url, usSpinnerService);
        }
        $scope.loadPlans();

        $scope.endPlanPeriodChosen = function (value) {
            if (value == 1) {
                $scope.currentPlan.NextSubscriptionPlanId = null;
            }
            else {
                $scope.currentPlan.NextSubscriptionPlanId = $scope.currentPlan.SubscriptionPlanId;
            }
        }

        $scope.createTransaction = function(vaultInfo, paymentValue, isRecurring, frequencyType) {
            return {
                Id: 0,
                CustomerId: vaultInfo.UserId,
                Number: "",
                PaymentValue: paymentValue,
                Status: 0,
                IsRecurring: isRecurring,
                FrequencyType: frequencyType
            };
        }

        $scope.managePlanUse = function (planId, paymentValue, isRecurring, frequencyType) {

            if (!planId) {
                $scope.isEdit = true;
            } else {
                $scope.isEdit = false;

            }

            $scope.planUse = $scope.isEdit ? angular.copy($scope.currentPlan) : {
                UserId: $scope.userData.UserData.Id,
                SubscriptionPlanId: planId
            };

            subscriptionsPlansHttpService.checkPaypal($http, $scope, $$ApiUrl + "/vault", usSpinnerService)
                .then(function (response) {
                    usSpinnerService.stop("spinner-1");
                    if (response != null) {
                        //
                        subscriptionsPlansHttpService.sendPayment($http, $scope, $$ApiUrl + "/transactions", usSpinnerService, {
                            Id: 0,
                            CustomerId: response.UserId,
                            Number: "",
                            PaymentValue: paymentValue,
                            Status: 0,
                            IsRecurring: isRecurring,
                            FrequencyType: frequencyType
                        }).then(function(response) {
                            subscriptionsPlansHttpService.managePlanUse($http, $scope, planUseUrl, usSpinnerService);
                        });
                        //
                        
                    } else {
                        var modalInstance = $uibModal.open({
                            templateUrl: 'PartialViews/CardInfoManagement.html',
                            controller: cardInfoManageController,
                            controllerAs: 'vm',
                            scope: $scope,
                            resolve: {
                                items: function () {
                                    return $scope.items;
                                }
                            }
                        });

                        modalInstance.result.then(function () {
                            subscriptionsPlansHttpService.managePlanUse($http, $scope, planUseUrl, usSpinnerService);
                        }, function () {
                            console.log('Modal dismissed at: ' + new Date());
                        });
                    }
                });

            usSpinnerService.stop("spinner-1");


        };
    };

    fccApp.controller("userSubscriptionsController", ["$scope", "$http", "$location", "$state", "$rootScope", "$window", "$cookies", "usSpinnerService", "Idle", "Keepalive", "$uibModal", "$locale", "subscriptionsPlansHttpService", userSubscriptionsController]);

    fccApp.directive('creditCardType',
        function () {
            var directive =
            {
                require: 'ngModel',
                link: function (scope, elm, attrs, ctrl) {
                    ctrl.$parsers.unshift(function (value) {
                        scope.cardInfo.type =
                            (/^5[1-5]/.test(value))
                            ? "mastercard"
                            : (/^4/.test(value))
                            ? "visa"
                            : (/^3[47]/.test(value))
                            ? 'amex'
                            : (/^6011|65|64[4-9]|622(1(2[6-9]|[3-9]\d)|[2-8]\d{2}|9([01]\d|2[0-5]))/.test(value))
                            ? 'discover'
                            : (/^(5018|5020|5038|6304|6759|6761|6763)[0-9]{8,15}$/.test(value))
                            ? 'maestro'
                            : undefined;
                        ctrl.$setValidity('invalid', !!scope.cardInfo.type);
                        return value;
                    });
                }
            }
            return directive;
        }
    );

    fccApp.directive('cardExpiration',
        function () {
            var directive =
            {
                require: 'ngModel',
                link: function (scope, elm, attrs, ctrl) {
                    scope.$watch('[cardInfo.month,cardInfo.year]',
                        function (value) {
                            ctrl.$setValidity('invalid', true);
                            if (scope.cardInfo.year == scope.currentYear && scope.cardInfo.month <= scope.currentMonth
                            ) {
                                ctrl.$setValidity('invalid', false);
                            }
                            return value;
                        },
                        true);
                }
            }
            return directive;
        }
    );

    fccApp.filter('range',
        function () {
            var filter =
                function (arr, lower, upper) {
                    for (var i = lower; i <= upper; i++) arr.push(i);
                    return arr;
                }
            return filter;
        }
    );
}())

