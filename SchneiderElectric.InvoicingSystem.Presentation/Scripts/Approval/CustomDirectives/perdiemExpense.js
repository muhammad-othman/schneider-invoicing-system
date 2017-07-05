// custom directive that displays self expense item of the expense
(function () {
    // the name of the angular js module
    var angularAppName = 'myApp';

    var invoiceApprovalApp = angular.module(angularAppName);

    var perdiemExpense = function (InvoiceSystemResources) {
        return {
            // can only be used as an element
            restrict: 'E',
            // empty isolated scope
            scope: {
                expenseItem: '=',
                expense: '='
            },
            templateUrl: InvoiceSystemResources.templates.perdiemExpenseUrl,
            controller: ['$scope', 'InvoiceSystemValues', 'InvoiceSystemExpenses', controller]
        }
    }

    var controller = function ($scope, InvoiceSystemValues, InvoiceSystemExpenses) {

        InvoiceSystemValues.getAllValues.then((values) => {
            $scope.currencies = values.currencies;
            $scope.rates = values.perdiemRates;
        });

        $scope.canBeEdited = InvoiceSystemExpenses.canBeEdited($scope.expenseItem, $scope.expense);

        $scope.opacity = $scope.canBeEdited ? '' : 'half-opacity';

    }

    invoiceApprovalApp.directive('perdiemExpense', ['InvoiceSystemResources', perdiemExpense]);

})();