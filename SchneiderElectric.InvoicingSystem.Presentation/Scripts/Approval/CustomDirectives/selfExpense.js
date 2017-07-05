// custom directive that displays self expenses items of the expense
(function () {

    // the name of the angular js module
    var angularAppName = 'myApp';

    var invoiceApprovalApp = angular.module(angularAppName);

    var selfExpense = function (InvoiceSystemResources) {
        return {
            // can only be used as an element
            restrict: 'E',
            // empty isolated scope
            scope: {
                expenseItem: '=',
                expense: '='
            },
            templateUrl: InvoiceSystemResources.templates.selfExpenseUrl,
            controller: ['$scope', 'InvoiceSystemValues', 'InvoiceSystemExpenses', controller]
        }
    }

    var controller = function ($scope, InvoiceSystemValues, InvoiceSystemExpenses) {

        InvoiceSystemValues.getAllValues.then((values) => {
            $scope.currencies = values.currencies;
            $scope.expenseTypes = values.expenseTypes;
        });

        $scope.canBeEdited = InvoiceSystemExpenses.canBeEdited($scope.expenseItem, $scope.expense);

        $scope.opacity = $scope.canBeEdited ? '' : 'half-opacity';

    }

    invoiceApprovalApp.directive('selfExpense', ['InvoiceSystemResources', selfExpense]);

})();