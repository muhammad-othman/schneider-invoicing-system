(function () {

    var myApp = angular.module('myApp', ['ngMaterial', 'ngTable']);

    myApp.config(function ($mdThemingProvider) {

        $mdThemingProvider.theme('default').primaryPalette('green').accentPalette('light-green');

    });

})();