var $$ApiUrl = "http://localhost/FCCPortalApi/api";
var fccApp = angular.module("fccApp", ["ui.router", 'ui.router.title', 'angularSpinner', 'ui.bootstrap', 'checklist-model', 'ngMessages', 'accessibleForm', 'ngCookies', 'ngIdle', 'ui.mask','angularjs-dropdown-multiselect','isteven-multi-select','vcRecaptcha']);

fccApp.run([
    '$rootScope', '$state', '$stateParams','$window', '$cookies',
    function ($rootScope, $state, $stateParams,$window, $cookies) {
        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;
    }
]);



fccApp.config(['usSpinnerConfigProvider', function (usSpinnerConfigProvider) {
    //usSpinnerConfigProvider.setTheme('bigBlue', { color: 'blue', radius: 20 });
    // usSpinnerConfigProvider.setTheme('smallRed', { color: 'red', radius: 6 });
}]);

// config session timeout
fccApp.config(['KeepaliveProvider', 'IdleProvider', function (KeepaliveProvider, IdleProvider) {
    IdleProvider.idle(1800);//при бездействии через 300 с появится окно
    IdleProvider.timeout(30);// которое в течении 30 сек. будет висеть
    KeepaliveProvider.interval(10);
}]);

fccApp.filter('myLimitTo', function () {
    return function (input, limit, begin) {
        return input.slice(begin, begin + limit);
    };
});

function twoDigits(d) {
    if (0 <= d && d < 10) return "0" + d.toString();
    if (-10 < d && d < 0) return "-0" + (-1 * d).toString();
    return d.toString();
}

/**
 * …and then create the method to output the date string as desired.
 * Some people hate using prototypes this way, but if you are going
 * to apply this to more than one Date object, having it as a prototype
 * makes sense.
 **/
Date.prototype.toMysqlFormat = function () {
    return this.getUTCFullYear() + "-" + twoDigits(1 + this.getUTCMonth()) + "-" + twoDigits(this.getUTCDate()) + " " + twoDigits(this.getUTCHours()) + ":" + twoDigits(this.getUTCMinutes()) + ":" + twoDigits(this.getUTCSeconds());
};
