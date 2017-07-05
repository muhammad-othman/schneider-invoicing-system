// custom directive that displays a button to reject an item
// this button is displayed only for EM aand Finance accounts
(function () {

    // the name of the angular js module
    var angularAppName = 'myApp';

    var invoiceApprovalApp = angular.module(angularAppName);

    var rejectExpenseItemButton = function () {
        return {
            // can only be used as an element
            restrict: 'E',
            // empty isolated scope
            scope: {
                expenseItem: '=',
                expense: '='
            },
            template: "<div layout='column'><md-button class='md-primary md-hue-1' ng-click='reject()' ng-hide='!canReject'>{{ ( expenseItem.Rejected ? 'Unreject' : 'Reject')}}</md-button></div>",
            controller: ['$scope', 'InvoiceSystemExpenses', 'InvoiceSystemAccounts', controller]
        }
    }

    var controller = function ($scope, InvoiceSystemExpenses, InvoiceSystemAccounts) {

        $scope.canReject = InvoiceSystemAccounts.canReject();

        //$scope.isExpenseRejected = InvoiceSystemExpenses.isRejected($scope.expense);

        $scope.reject = function () {
            $scope.expenseItem.Rejected = !$scope.expenseItem.Rejected;
        }
    }

    invoiceApprovalApp.directive('rejectExpenseItemButton', [rejectExpenseItemButton]);

})();