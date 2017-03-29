fccApp.config(function ($stateProvider, $urlRouterProvider, $locationProvider) {

    $urlRouterProvider.otherwise("/login");

    //main page (abstract view)
    $stateProvider
        .state("main", {
            url: "",
            abstract: true,
            template: "<div ui-view></div>",
            data: { pageTitle: "FCC Portal" },
            onEnter: function ($window) { $window.document.title = "FCC Portal"; }

        })

        //login page
        .state("main.login", {
            url: "/login",
            templateUrl: "PartialViews/Login.html",
            data: { pageTitle: "Sign In" },
            onEnter: function ($window, $cookies, $state) {

                //                if ($cookies.getObject("AuthData")) {
                //                    $window.sessionStorage.setItem("UserData", JSON.stringify($cookies.getObject("UserData")));
                //                    $window.sessionStorage.setItem("AuthData", JSON.stringify($cookies.getObject("AuthData")));
                //                    $window.sessionStorage.setItem("PhoneMask", JSON.stringify($cookies.getObject("PhoneMask")));
                //                    $state.go("main.dashboard");
                //                }
                //                $window.document.title = "Авторизация";
            }


        })

        ////dashboard container
        .state("main.dashboard", {
            url: "/dashboard",
            templateUrl: "PartialViews/Dashboard.html",
            onEnter: function ($window) {
                $window.document.title = "Dashboard";
            }
        })

        //        ///user managments
        .state("main.dashboard.users", {
            url: "/users",
            templateUrl: "PartialViews/Users.html",
            onEnter: function ($window, $state) {
                if (!$window.sessionStorage.getItem("UserData")) {
                    $state.go("main.login")
                }
                $window.document.title = "Users";
            }
        })

        .state("main.dashboard.adminSettings", {
            url: "/adminsettings",
            templateUrl: "PartialViews/AdminSettings.html",
            onEnter: function ($window, $state) {
                if (!$window.sessionStorage.getItem("UserData")) {
                    //$state.go("main.login")
                }
                $window.document.title = "Admin Settings";
            }
        })

        .state("main.dashboard.systemSettings", {
            url: "/systemsettings",
            templateUrl: "PartialViews/SystemSettings.html",
            onEnter: function ($window, $state) {
                if (!$window.sessionStorage.getItem("UserData")) {
                    //$state.go("main.login")
                }
                $window.document.title = "Admin Settings";
            }
        })

                .state("main.dashboard.adminStatistics", {
            url: "/adminstatistics",
            templateUrl: "PartialViews/AdminStatistics.html",
            onEnter: function ($window, $state) {
                if (!$window.sessionStorage.getItem("UserData")) {
                    //$state.go("main.login")
                }
                $window.document.title = "Admin Statistics";
            }
        })
    //        .state("main.dashboard.addUser", {
    //            url: "/adduser",
    //            params: { operation: "add", userId: null },
    //            templateUrl: "PartialViews/UserManagment.html",
    //            onEnter: function ($window, $state) {
    //                if (!$window.sessionStorage.getItem("UserData")) {
    //                    $state.go("main.login")
    //                }
    //                $window.document.title = "Управление пользователем";
    //            }
    //        })
    //        .state("main.dashboard.companies", {
    //            url: "/companies",
    //            templateUrl: "PartialViews/Companies.html",
    //            onEnter: function ($window, $state) {
    //                if (!$window.sessionStorage.getItem("UserData")) {
    //                    $state.go("main.login")
    //                }
    //                $window.document.title = "Заведения";
    //            }
    //        })
    //        .state("main.dashboard.companyDetails", {
    //            url: "/companyDetails/:companyId",
    //            templateUrl: "PartialViews/CompanyDetails.html",
    //            onEnter: function ($window, $state) {
    //                if (!$window.sessionStorage.getItem("UserData")) {
    //                    $state.go("main.login")
    //                }
    //                $window.document.title = "Подробно о заведении";
    //            }
    //        })
    //        .state("main.dashboard.menus", {
    //            url: "/menus",
    //            templateUrl: "PartialViews/Menus.html",
    //            onEnter: function ($window, $state) {
    //                if (!$window.sessionStorage.getItem("UserData")) {
    //                    $state.go("main.login")
    //                }
    //                $window.document.title = "Меню";
    //            }
    //        })
    //
    //        .state("main.dashboard.menuDetails", {
    //            url: "/menuDetails/:menuId",
    //            templateUrl: "PartialViews/MenuDetails.html",
    //            onEnter: function ($window, $state) {
    //                if (!$window.sessionStorage.getItem("UserData")) {
    //                    $state.go("main.login")
    //                }
    //                $window.document.title = "Подробно о меню";
    //            }
    //        })
    //
    //        .state("main.dashboard.dishDetails", {
    //            url: "/dishDetails/:dishId/:menuId",
    //            templateUrl: "PartialViews/DishManagment.html",
    //            onEnter: function ($window, $state) {
    //                if (!$window.sessionStorage.getItem("UserData")) {
    //                    $state.go("main.login")
    //                }
    //                $window.document.title = "Карточка блюда";
    //            }
    //        })
    //
    //        .state("main.dashboard.catalogs", {
    //            url: "/catalogs",
    //            templateUrl: "PartialViews/Catalogs.html",
    //            onEnter: function ($window, $state) {
    //                if (!$window.sessionStorage.getItem("UserData")) {
    //                    $state.go("main.login")
    //                }
    //                $window.document.title = "Справочники";
    //            }
    //        })
    //        .state("main.dashboard.settings", {
    //            url: "/settings",
    //            templateUrl: "PartialViews/Settings.html",
    //            onEnter: function ($window, $state) {
    //                if (!$window.sessionStorage.getItem("UserData")) {
    //                    $state.go("main.login")
    //                }
    //                $window.document.title = "Настройки";
    //            }
    //        })
    //        .state("main.dashboard.orders", {
    //            url: "/orders",
    //            templateUrl: "PartialViews/Orders.html",
    //            onEnter: function ($window, $state) {
    //                if (!$window.sessionStorage.getItem("UserData")) {
    //                    $state.go("main.login")
    //                }
    //                $window.document.title = "Заказы";
    //            }
    //        })
    //        .state("main.dashboard.orderDetails", {
    //            url: "/orders/:orderId",
    //            templateUrl: "PartialViews/OrdersDetails.html",
    //            onEnter: function ($window, $state) {
    //                if (!$window.sessionStorage.getItem("UserData")) {
    //                    $state.go("main.login")
    //                }
    //                $window.document.title = "Детали заказа";
    //            },
    //            params: { currentOrder: null, menuData: null, orders: null, sightsAddressData: null }
    //        })
    //        .state("main.dashboard.chat", {
    //            url: "/chat",
    //            templateUrl: "PartialViews/Chat.html",
    //            onEnter: function ($window, $state) {
    //                if (!$window.sessionStorage.getItem("UserData")) {
    //                    $state.go("main.login")
    //                }
    //                $window.document.title = "Сообщения";
    //            }
    //        })
    //
    //        //справочники
    //        .state("main.dashboard.products", {
    //            url: "/products",
    //            templateUrl: "PartialViews/Products.html",
    //            onEnter: function ($window, $state) {
    //                if (!$window.sessionStorage.getItem("UserData")) {
    //                    $state.go("main.login")
    //                }
    //                $window.document.title = "Продукты";
    //            }
    //        })
    //
    //        .state("main.dashboard.menuCategories", {
    //            url: "/menuCategories",
    //            templateUrl: "PartialViews/MenuCategories.html",
    //            onEnter: function ($window, $state) {
    //                if (!$window.sessionStorage.getItem("UserData")) {
    //                    $state.go("main.login")
    //                }
    //                $window.document.title = "Категории продуктов";
    //            }
    //        })
    //
    //        .state("main.dashboard.variators", {
    //            url: "/variators",
    //            templateUrl: "PartialViews/Variators.html",
    //            onEnter: function ($window, $state) {
    //                if (!$window.sessionStorage.getItem("UserData")) {
    //                    $state.go("main.login")
    //                }
    //                $window.document.title = "Вариаторы";
    //            }
    //        })
    //
    //        .state("main.dashboard.modificators", {
    //            url: "/modificators",
    //            templateUrl: "PartialViews/Modificators.html",
    //            onEnter: function ($window, $state) {
    //                if (!$window.sessionStorage.getItem("UserData")) {
    //                    $state.go("main.login")
    //                }
    //                $window.document.title = "Модификаторы";
    //            }
    //        })
    //
    //        .state("main.dashboard.ingredients", {
    //            url: "/ingredients",
    //            templateUrl: "PartialViews/Ingredients.html",
    //            onEnter: function ($window, $state) {
    //                if (!$window.sessionStorage.getItem("UserData")) {
    //                    $state.go("main.login")
    //                }
    //                $window.document.title = "Ингредиенты";
    //            }
    //        })
    //
    //        .state("main.dashboard.productTypes", {
    //            url: "/productTypes",
    //            templateUrl: "PartialViews/ProductTypes.html",
    //            onEnter: function ($window, $state) {
    //                if (!$window.sessionStorage.getItem("UserData")) {
    //                    $state.go("main.login")
    //                }
    //                $window.document.title = "Типы товаров";
    //            }
    //        })
    //
    //        .state("main.dashboard.catalogWeights", {
    //            url: "/catalogWeights",
    //            templateUrl: "PartialViews/CatalogWeights.html",
    //            onEnter: function ($window, $state) {
    //                if (!$window.sessionStorage.getItem("UserData")) {
    //                    $state.go("main.login")
    //                }
    //                $window.document.title = "Каталог весов";
    //            }
    //        })
    //
    //        .state("main.dashboard.catalogCurrency", {
    //            url: "/catalogCurrency",
    //            templateUrl: "PartialViews/CatalogCurrency.html",
    //            onEnter: function ($window, $state) {
    //                if (!$window.sessionStorage.getItem("UserData")) {
    //                    $state.go("main.login")
    //                }
    //                $window.document.title = "Каталог валют";
    //            }
    //        })
    //
    //        .state("main.dashboard.tare", {
    //            url: "/catalogTare",
    //            templateUrl: "PartialViews/CatalogTare.html",
    //            onEnter: function ($window, $state) {
    //                if (!$window.sessionStorage.getItem("UserData")) {
    //                    $state.go("main.login")
    //                }
    //                $window.document.title = "Каталог Тары";
    //            }
    //        })
    //
    //        .state("main.dashboard.mainPageGallery", {
    //            url: "/ClientMainPageGallery",
    //            templateUrl: "PartialViews/ClientMainPageGallery.html",
    //            onEnter: function ($window, $state) {
    //                if (!$window.sessionStorage.getItem("UserData")) {
    //                    $state.go("main.login")
    //                }
    //                $window.document.title = "Галерея";
    //            }
    //        })
    //
    //        .state("main.dashboard.sights", {
    //            url: "/Sights",
    //            templateUrl: "PartialViews/Sights.html",
    //            onEnter: function ($window, $state) {
    //                if (!$window.sessionStorage.getItem("UserData")) {
    //                    $state.go("main.login")
    //                }
    //                $window.document.title = "Каталог достопримечательностей";
    //            }
    //        })
    //
    //    .state("main.dashboard.variatorParameters", {
    //        url: "/variatorParameters/:variatorId",
    //        templateUrl: "PartialViews/VariatorParameters.html",
    //        onEnter: function ($window, $state) {
    //            if (!$window.sessionStorage.getItem("UserData")) {
    //                $state.go("main.login")
    //            }
    //            $window.document.title = "Параметры вариатора";
    //        }
    //    })
    //
    //    .state("main.dashboard.variatorParameterFields", {
    //        url: "/variatorParameterFields/:variatorParameterId",
    //        templateUrl: "PartialViews/VariatorParameterFields.html",
    //        onEnter: function ($window, $state) {
    //            if (!$window.sessionStorage.getItem("UserData")) {
    //                $state.go("main.login")
    //            }
    //            $window.document.title = "Параметры полей";
    //        }
    //    })
    //
    //    .state("main.dashboard.userInfo", {
    //        url: "/users/:userId",
    //        templateUrl: "PartialViews/UserInfo.html",
    //        onEnter: function ($window) {
    //            if (!$window.sessionStorage.getItem("UserData")) {
    //                $state.go("main.login")
    //            }
    //            $window.document.title = "Параметры пользователя";
    //        }
    //    })

    // use the HTML5 History API
    //$locationProvider.html5Mode(true);
});