var myApp = angular.module('myApp', ['ngMaterial', 'ngMessages']);

myApp.config(function($mdThemingProvider) {
    $mdThemingProvider.theme('default').primaryPalette('green').accentPalette('light-green');
});

myApp.controller('loginController', function($scope, $mdDialog, $mdSidenav, $http) {

    $scope.Login = function (id) { Login(id, $http) };
});

function Login(id, $http) {

};