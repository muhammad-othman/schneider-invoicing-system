// custom directive that displays general info of the expense (like engineer name, project name, ...)
(function () {

    // the name of the angular js module
    var angularAppName = 'myApp';

    var invoiceApprovalApp = angular.module(angularAppName);

    var expenseInformation = function (InvoiceSystemResources) {
        return {
            // can only be used as an element
            restrict: 'E',
            // empty isolated scope
            scope: {
                expense: '='
            },
            templateUrl: InvoiceSystemResources.templates.expenseInformationUrl,
            controller: ['$scope', 'InvoiceSystemValues', 'InvoiceSystemResources', 'InvoiceSystemAccounts', controller]
        }
    }

    var controller = function ($scope, InvoiceSystemValues, InvoiceSystemResources, InvoiceSystemAccounts) {

        InvoiceSystemValues.getAllValues.then((values) => {
            $scope.countries = values.countryList;
        });

        $scope.errorMessage = InvoiceSystemResources.messages.dataLoading.failure;

        $scope.hasComments = $scope.expense.RejectedComments && $scope.expense.RejectedComments.length > 0;
        
        if ($scope.hasComments) {
            $scope.latestComment = $scope.expense.RejectedComments
                .find(comment => comment.DateRejected.getTime() == Math.max
                    .apply(null, $scope.expense.RejectedComments.map(comment => comment.DateRejected))
                )
        }

        $scope.showComments = $scope.hasComments && InvoiceSystemAccounts.isAtProjectAdmin();

        $scope.canBeEdited = false;

        if ($scope.expense) {
            $scope.showError = false;
            $scope.showItems = true;
        }
        else {
            $scope.showError = true;
            $scope.showItems = false;
        }
    }

    invoiceApprovalApp.directive('expenseInformation', ['InvoiceSystemResources', expenseInformation]);

})();