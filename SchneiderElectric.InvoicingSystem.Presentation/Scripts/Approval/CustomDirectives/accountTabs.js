// custom directive that displays tabs for each expense state depending on the account type
// for example, a PA will have three tabs (Active, Cancelled and Rejected)
(function () {

    // the name of the angular js module
    var angularAppName = 'myApp';

    var invoiceApprovalApp = angular.module(angularAppName);

    var accountTabs = function (InvoiceSystemResources) {
        return {
            // can only be used as an element
            restrict: 'E',
            // empty isolated scope
            scope: {},
            templateUrl: InvoiceSystemResources.templates.accountTabsUrl,
            controller: ['$scope', 'InvoiceSystemAccounts', 'InvoiceSystemExpenses', controller]
        }
    }

    var controller = function ($scope, InvoiceSystemAccounts, InvoiceSystemExpenses) {
        $scope.showCancelled = InvoiceSystemAccounts.hasCancelledExpenses();
        $scope.showRejected = InvoiceSystemAccounts.hasRejectedExpenses();

        $scope.clickTab = clickTab;
    }

    var clickTab = function (expenseType) {

        if (expenseType == 'cancelled')
            $('md-tab-item span:contains("Cancelled")').removeClass('red-color');
        else if (expenseType == 'rejected')
            $('md-tab-item span:contains("Rejected")').removeClass('red-color');
        else if (expenseType == 'all')
            $('md-tab-item span:contains("Pending")').removeClass('red-color');


    }

    invoiceApprovalApp.directive('accountTabs', ['InvoiceSystemResources', accountTabs]);

})();