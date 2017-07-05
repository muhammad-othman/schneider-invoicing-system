// custom directive that displays a spinning wheel while loading data
// when data is loaded, it's displayed in an ng-table
(function () {

    // the name of the angular js module
    var angularAppName = 'myApp';

    var invoiceApprovalApp = angular.module(angularAppName);

    var expensesTable = function (InvoiceSystemResources) {
        return {
            // can only be used as an element
            restrict: 'E',
            scope: {
                // type of the expenses to be loaded (Active, Rejected or Cancelled)
                // @ is used for binding with an attribute named expense-type
                expenseType: '@expenseType'
            },
            templateUrl: InvoiceSystemResources.templates.expensesTableUrl,
            controller: ['$scope', 'InvoiceSystemExpenses', 'InvoiceSystemResources', 'NgTableParams', 'InvoiceSystemAccounts', 'InvoiceSystemValues', 'ViewExpense', controller],
            link: link
        }
    }

    var controller = function ($scope, InvoiceSystemExpenses, InvoiceSystemResources, NgTableParams, InvoiceSystemAccounts, InvoiceSystemValues, ViewExpense) {
        
        $scope.showSpinner = true;
        // this column is not visible to the project admin
        // it would be meaningless
        $scope.showProjectName = InvoiceSystemAccounts.canViewProjectNames();
        $scope.errorMessage = InvoiceSystemResources.messages.dataLoading.failure;

        initializeCountriesList($scope, InvoiceSystemValues);

        $scope.states = InvoiceSystemExpenses.getExpenseStatesAsArray();

        // $scope._data is a promise object
        $scope._data = loadData($scope.expenseType, InvoiceSystemExpenses);

        $scope._data
            .then((values) => loaded(values, $scope))
            .catch((error) => failed(error, $scope));

        // ngTable initialization
        $scope.initializeNgTable = () => initializeNgTable($scope, NgTableParams);

        $scope.viewExpense = function (expense) {
            if (!$scope.disableViewingExpenseItems) {
                // shows the dialog that contains the expense details
                ViewExpense.show(expense);
            }
        }

        if ($scope.expenseType.toLowerCase().includes('all')) {
            $scope.disableViewingExpenseItems = true;
            $scope.atAllTab = true;
        }

        $scope.getStateDisplayName = (expense) => InvoiceSystemExpenses.getExpenseState(expense);

    }

    var initializeCountriesList = function ($scope, InvoiceSystemValues) {

        $scope.countries = [];

        InvoiceSystemValues.getAllValues.then(

            (values) => Array.prototype.push.apply(
                $scope.countries, values.countryList.map(c => ({ id: c.Country, title: c.Country }))
            )

        );

    }

    var initializeNgTable = function ($scope, NgTableParams) {

        $scope.expensesTable = new NgTableParams(
            {
                page: 1,
                count: 25
            },
            {
                // this is bound to $scope.data which contains the actual values, not _data which is a promise
                dataset: $scope.data
            }
        );

    }

    var loadData = function (expenseType, InvoiceSystemExpenses) {

        if (expenseType == 'cancelled')
            return InvoiceSystemExpenses.getCancelled;
        else if (expenseType == 'rejected')
            return InvoiceSystemExpenses.getRejected;
        else if (expenseType == 'all')
            return InvoiceSystemExpenses.getAll;
        // default value is for getting active expenses data
        else
            return InvoiceSystemExpenses.getActive;

    }

    var link = function ($scope, element /*, attribute, controller*/) {

        $(document).ready(function () {

            $(element).find("tr").click(function () {
                $(this).addClass("selected").siblings().removeClass("selected");
            })

        })

    }

    var loaded = function (values, $scope) {

        // can we delete the _data promise ?!!

        $scope.data = values;

        setTabColor($scope.expenseType, values);
        // watch for changes in the scope (this will typically be caused by changes in the invoice promise data (getActive for example) that's linked with the scope)
        // this is used to call table.reload() to redraw the table whenever the data changes in size (by adding a new item or removing it when cancelling, recalling, or submitting an expense)    
        $scope.$watch(() => $scope.data.length, function () { recheckTableVisibility($scope); $scope.expensesTable.reload(); }, true);

        // this way we keep the original array object intact
        // because the ngTable is bound to this object
        $scope.initializeNgTable();

    }

    // this function is used to set the color of the account tabs to get indication that they contain data
    var setTabColor = function (expenseType, values) {

        if (values.length == 0)
            return;

        $('md-pagination-wrapper').addClass('width-auto');

        if (expenseType == 'cancelled')
            $('md-tab-item span:contains("Cancelled")').addClass('red-color');
        else if (expenseType == 'rejected')
            $('md-tab-item span:contains("Rejected")').addClass('red-color');
        else if (expenseType == 'all')
            $('md-tab-item span:contains("Pending")').addClass('red-color');

    }

    var recheckTableVisibility = function ($scope) {

        if ($scope.data.length > 0) {

            showTable($scope);

        }
        else {

            $scope.errorMessage = `You have no ${$scope.expenseType} expenses`;
            hideTable($scope);

        }

    }

    var showTable = function ($scope) {
        // hide the spinning wheel
        $scope.showSpinner = false;
        // show the ng-table
        $scope.showTable = true;
        // hide error
        $scope.showError = false;
    }

    var failed = function (error, $scope) {

        console.log(error);

        hideTable($scope);

    }

    var hideTable = function ($scope) {

        // hide the spinning wheel
        $scope.showSpinner = false;
        // show the ng-table
        $scope.showTable = false;
        // hide error
        $scope.showError = true;

    }

    invoiceApprovalApp.directive('expensesTable', ['InvoiceSystemResources', expensesTable]);

})();