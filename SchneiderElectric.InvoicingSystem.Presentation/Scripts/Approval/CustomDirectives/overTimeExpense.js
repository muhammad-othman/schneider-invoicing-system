// custom directive that displays overtime expense item of the expense
(function () {

    // the name of the angular js module
    var angularAppName = 'myApp';

    var invoiceApprovalApp = angular.module(angularAppName);

    var overTimeExpense = function (InvoiceSystemResources) {
        return {
            // can only be used as an element
            restrict: 'E',
            // empty isolated scope
            scope: {
                expenseItem: '=',
                expense: '='
            },
            templateUrl: InvoiceSystemResources.templates.overTimeExpenseUrl,
            controller: ['$scope', 'InvoiceSystemValues', 'InvoiceSystemExpenses', controller]
        }
    }

    var controller = function ($scope, InvoiceSystemValues, InvoiceSystemExpenses) {

        InvoiceSystemValues.getAllValues.then((values) => {
            $scope.currencies = values.currencies;
            $scope.categories = values.overTimeExpenseCategories;
        });

        $scope.canBeEdited = InvoiceSystemExpenses.canBeEdited($scope.expenseItem, $scope.expense);

        $scope.opacity = $scope.canBeEdited ? '' : 'half-opacity';

    }

    invoiceApprovalApp.directive('overTimeExpense', ['InvoiceSystemResources', overTimeExpense]);

})();