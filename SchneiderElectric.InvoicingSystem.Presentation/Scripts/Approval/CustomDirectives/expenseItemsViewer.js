// custom directive that displays expense items of an expense (for a certain type)
// it uses ng-repeat for displaying the items
// use it like:
//<expense-items-viewer expense="expense" type="SelfExpense"></expense-items-viewer>
// where:
// _expense is the object (in a $scope of another controller) that contains the sub expenses items
// type is the name of the property (array) that holds the items
(function () {

    // the name of the angular js module
    var angularAppName = 'myApp';

    var invoiceApprovalApp = angular.module(angularAppName);

    var expenseItemsViewer = function (InvoiceSystemResources) {
        return {
            // can only be used as an element
            restrict: 'E',
            // isolated scope
            scope: {
                // used to get the expense that contains the list of sub-expenses (expense items like selfExpenses, ...) to be viewed
                expense: '=',
                // used to get the type of the expense items to be viewed
                // use the name of the expense items property (like selfExpenses, overTimeExpenses, ...)
                type: '@'
            },
            templateUrl: InvoiceSystemResources.templates.expenseItemsViewerUrl,
            controller: ['$scope', 'InvoiceSystemResources', controller]
        }
    }

    var controller = function ($scope, InvoiceSystemResources) {

        $scope.errorMessage = InvoiceSystemResources.messages.dataLoading.empty;

        $scope.expenseItems = $scope.expense[$scope.type];

        if ($scope.expenseItems && $scope.expenseItems.length > 0) {
            $scope.showError = false;
            $scope.showItems = true;
        }
        else {
            $scope.showError = true;
            $scope.showItems = false;
        }

        $scope.showSelfExpenses = $scope.type.toLowerCase().includes('self');
        $scope.showPerdiemExpenses = $scope.type.toLowerCase().includes('perdiem');
        $scope.showOverTimeExpenses = $scope.type.toLowerCase().includes('overtime');
    }

    invoiceApprovalApp.directive('expenseItemsViewer', ['InvoiceSystemResources', expenseItemsViewer]);

})();