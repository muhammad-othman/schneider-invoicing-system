(function () {

    // the name of the angular js module
    var angularAppName = 'myApp';

    var myApp = angular.module(angularAppName);

    myApp.controller('accountController', ['$scope', 'InvoiceSystemAccounts', function ($scope, InvoiceSystemAccounts) {

        // initialization
        $scope.initEmployeeType = (employeeType) => {
            InvoiceSystemAccounts.setType(employeeType);
            $scope.accountDisplayName = InvoiceSystemAccounts.getDisplayName();
        }

    }]);

})();