﻿fccApp.config(function ($stateProvider, $urlRouterProvider, $locationProvider) {

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
                                $window.document.title = "Sign In";
            }

        })

        //registration
        .state("main.registration", {
            url: "/registration",
            templateUrl: "PartialViews/UserRegistration.html",
            data: { pageTitle: "User Registration" },
            onEnter: function ($window, $cookies, $state) {
                $window.document.title = "User Registration";
            }
        })

             //restore
        .state("main.restore", {
            url: "/restore",
            templateUrl: "PartialViews/UserRestore.html",
            data: { pageTitle: "User Restore Account" },
            onEnter: function ($window, $cookies, $state) {
                $window.document.title = "User Restore Account";
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

        .state("main.dashboard.profile", {
            url: "/profile",
            templateUrl: "PartialViews/UserProfile.html",
            onEnter: function ($window, $state) {
                $window.document.title = "User Profile";
            }
        })

        .state("main.dashboard.store", {
            url: "/store",
            templateUrl: "PartialViews/Store.html",
            onEnter: function ($window, $state) {
                $window.document.title = "Store";
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

         .state("main.dashboard.single", {
            url: "/single",
            templateUrl: "PartialViews/SingleFileConversion.html",
            onEnter: function ($window, $state) {
                if (!$window.sessionStorage.getItem("UserData")) {
                    //$state.go("main.login")
                }
                $window.document.title = "Single File Conversion";
            }
        })

        .state("main.dashboard.singlelibrary", {
            url: "/singlelibrary",
            templateUrl: "PartialViews/SingleLibrary.html",
            onEnter: function ($window, $state) {
                if (!$window.sessionStorage.getItem("UserData")) {
                    //$state.go("main.login")
                }
                $window.document.title = "Single File Library";
            }
        })

        .state("main.dashboard.singlesettings", {
            url: "/singlesettings",
            templateUrl: "PartialViews/SingleSettings.html",
            onEnter: function ($window, $state) {
                if (!$window.sessionStorage.getItem("UserData")) {
                    //$state.go("main.login")
                }
                $window.document.title = "Single File Settings";
            }
        })

        .state("main.dashboard.singleprofile", {
            url: "/singleprofile",
            templateUrl: "PartialViews/SingleProfile.html",
            onEnter: function ($window, $state) {
                if (!$window.sessionStorage.getItem("UserData")) {
                    //$state.go("main.login")
                }
                $window.document.title = "Single File Profile";
            }
        })

         .state("main.dashboard.batch", {
            url: "/batch",
            templateUrl: "PartialViews/BatchConversion.html",
            onEnter: function ($window, $state) {
                if (!$window.sessionStorage.getItem("UserData")) {
                    //$state.go("main.login")
                }
                $window.document.title = "Batch File Conversion";
            }
        })

        .state("main.dashboard.batchlibrary", {
            url: "/batchlibrary",
            templateUrl: "PartialViews/BatchLibrary.html",
            onEnter: function ($window, $state) {
                if (!$window.sessionStorage.getItem("UserData")) {
                    //$state.go("main.login")
                }
                $window.document.title = "Batch File Library";
            }
        })

        .state("main.dashboard.batchsettings", {
            url: "/batchsettings",
            templateUrl: "PartialViews/BatchSettings.html",
            onEnter: function ($window, $state) {
                if (!$window.sessionStorage.getItem("UserData")) {
                    //$state.go("main.login")
                }
                $window.document.title = "Batch File Settings";
            }
        })

        .state("main.dashboard.batchprofile", {
            url: "/batchprofile",
            templateUrl: "PartialViews/BatchProfile.html",
            onEnter: function ($window, $state) {
                if (!$window.sessionStorage.getItem("UserData")) {
                    //$state.go("main.login")
                }
                $window.document.title = "Batch File Profile";
            }
        })


        

        .state("main.dashboard.emaillibrary", {
            url: "/emaillibrary",
            templateUrl: "PartialViews/EmailLibrary.html",
            onEnter: function ($window, $state) {
                if (!$window.sessionStorage.getItem("UserData")) {
                    //$state.go("main.login")
                }
                $window.document.title = "Email File Library";
            }
        })

        .state("main.dashboard.emailsettings", {
            url: "/emailsettings",
            templateUrl: "PartialViews/EmailSettings.html",
            onEnter: function ($window, $state) {
                if (!$window.sessionStorage.getItem("UserData")) {
                    //$state.go("main.login")
                }
                $window.document.title = "Email File Settings";
            }
        })

       
        .state("main.dashboard.ftplibrary", {
            url: "/ftplibrary",
            templateUrl: "PartialViews/FtpLibrary.html",
            onEnter: function ($window, $state) {
                if (!$window.sessionStorage.getItem("UserData")) {
                    //$state.go("main.login")
                }
                $window.document.title = "FTP File Library";
            }
        })

        .state("main.dashboard.ftpsettings", {
            url: "/ftpsettings",
            templateUrl: "PartialViews/FtpSettings.html",
            onEnter: function ($window, $state) {
                if (!$window.sessionStorage.getItem("UserData")) {
                    //$state.go("main.login")
                }
                $window.document.title = "FTP File Settings";
            }
        })

   
    // use the HTML5 History API
    //$locationProvider.html5Mode(true);
});