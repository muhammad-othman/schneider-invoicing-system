// this file contains the angular js service used for getting expenses data from the back-end WebApi from the expenses controller
// it depends on the InvoiceSystemResources service (value)
(function () {

    // the name of the angular js module
    var angularAppName = 'myApp';

    var invoiceApprovalApp = angular.module(angularAppName);

    var ExpenseStates = {};

    var Constructor = function (InvoiceSystemResources, $http, InvoiceSystemAccounts, InvoiceSystemValues, $mdToast) {

        ExpenseStates = InvoiceSystemResources.ExpenseStates;

        getExpenses($http, InvoiceSystemResources);

        this.getActive = getActive;
        this.getCancelled = getCancelled;
        this.getRejected = getRejected;
        this.getAll = getAll;

        // submit expense is used for (approve, reject)
        // this depends on whether the expense has a rejected item in it or not.
        this.submitExpense = (data, query) => postData($http, InvoiceSystemResources.expenses.post.submitUrl, data, query, $mdToast);
        this.cancelExpense = (query) => postData($http, InvoiceSystemResources.expenses.post.cancelUrl, null, query, $mdToast);
        this.recallExpense = (query) => postData($http, InvoiceSystemResources.expenses.post.recallUrl, null, query, $mdToast);

        this.getExpenseState = (expense) => getExpenseState(expense);
        this.getExpenseStatesAsArray = getExpenseStatesAsArray;

        this.isRejected = (expense) => isRejected(expense);
        this.isCancelled = (expense) => isCancelled(expense);
        this.canBeEdited = (expenseItem, expense) => canBeEdited(expenseItem, expense, InvoiceSystemAccounts.isAtProjectAdmin());

        this.addToActive = (expense) => addToActive(expense, this.getActive);
        this.addToCancelled = (expense) => addToCancelled(expense, this.getCancelled);
        this.addToRejected = (expense) => addToRejected(expense, this.getRejected);

        this.removeFromActive = (expense) => removeFromActive(expense, this.getActive);
        this.removeFromCancelled = (expense) => removeFromCancelled(expense, this.getCancelled);
        this.removeFromRejected = (expense) => removeFromRejected(expense, this.getRejected);

        // states should be named as follows:
        // [(at)Engineer, (at)ProjectAdministrator, Cancelled, Rejected, (at)EM-EngineeringManager, (at)Finance]
        // note that these functions will only work with the Project Admin
        this.setAsCancelled = (expense) => setState(expense, ExpenseStates.cancelled);
        this.setAsRejected = (expense) => setState(expense, ExpenseStates.rejected);
        // project admin is the only state that contains the letter (P)
        this.setAsActive = (expense) => setState(expense, ExpenseStates.atPA_Active);

        this.isBeingRejected = (expense) => isBeingRejected(expense, InvoiceSystemAccounts);

        this.getActiveExpensesCount = () => getActiveExpensesCount(this.getActive);

        this.getFiles = (expenseId) => getData($http, InvoiceSystemResources.expenses.get.filesUrl, { key: 'ExpenseId', value: expenseId });
    }

    var flag = true;
    var getActive = {};
    var getCancelled = {};
    var getRejected = {};
    var getAll = {};
    var getExpenses = function ($http, InvoiceSystemResources) {

        // this is done to load he data only once
        if (flag) {
            flag = false;

            getActive = getData($http, InvoiceSystemResources.expenses.get.activeUrl);
            getCancelled = getData($http, InvoiceSystemResources.expenses.get.cancelledUrl);
            getRejected = getData($http, InvoiceSystemResources.expenses.get.rejectedUrl);
            getAll = getData($http, InvoiceSystemResources.expenses.get.allUrl);

        }

    }

    var getExpenseStatesAsArray = function (forDisplay = true) {

        states = [];

        for (state in ExpenseStates) {
            if (ExpenseStates.hasOwnProperty(state)) {

                state = ExpenseStates[state]

                if(forDisplay)
                    states.push({ id: state.key, title: state.displayValue });
                else
                    states.push(state)

            }
        }

        return states;

    }

    var isBeingRejected = function (expense, InvoiceSystemAccounts) {

        if (InvoiceSystemAccounts.isAtProjectAdmin())
            return false;

        if (expense.SelfExpenses.find(expense => expense.Rejected == true))
            return true;
        else if (expense.PerdiemExpenses.find(expense => expense.Rejected == true))
            return true;
        else if (expense.OverTimeExpenses.find(expense => expense.Rejected == true))
            return true;
        else
            return false;
    }

    var getExpenseState = function (expense) {

        var states = getExpenseStatesAsArray(false);
        return states.find((state) => state.key == expense.ExpenseState).displayValue;

    }

    var setState = function (expense, expenseState) {

        expense.ExpenseState = expenseState.key;

    }

    var addToActive = function (expense, activeExpensesPromise) {
        return activeExpensesPromise.then((values) => values.push(expense));
    }

    var addToCancelled = function (expense, cancelledExpensesPromise) {
        return cancelledExpensesPromise.then((values) => values.push(expense));
    }

    var addToRejected = function (expense, rejectedExpensesPromise) {
        return rejectedExpensesPromise.then((values) => values.push(expense));
    }

    var removeFromActive = function (expense, activeExpensesPromise) {
        return activeExpensesPromise.then((values) => removeObjectFromArray(expense, values));
    }

    var removeFromCancelled = function (expense, cancelledExpensesPromise) {
        return cancelledExpensesPromise.then((values) => removeObjectFromArray(expense, values));
    }

    var removeFromRejected = function (expense, rejectedExpensesPromise) {
        return rejectedExpensesPromise.then((values) => removeObjectFromArray(expense, values));
    }

    var removeObjectFromArray = function (object, array, propertyName = 'ExpenseId') {
        var index = array.findIndex(element => element[propertyName] == object[propertyName]);
        array.splice(index, 1);
    }

    var canBeEdited = function (expenseItem, expense, isAtProjectAdmin) {

        var isExpenseRejected = isRejected(expense);

        // if at PA and is not rejected, it can not be edited, else it can be
        if (isAtProjectAdmin && !expenseItem.Rejected && isExpenseRejected)
            return false;
        else
            return true;

    }

    var isCancelled = function (expense) {

        return expense.ExpenseState == ExpenseStates.cancelled.key;

    }

    var isRejected = function (expense) {

        return expense.ExpenseState == ExpenseStates.rejected.key;

    }

    // this function is used to convrt dates in the expense from string to js Date objects
    // this is used to facilitate binding in the ngTable (sorting, gettingLatestComment, ...)
    var fromStringToDate = function (values) {

        if (values) {

            values.forEach(expense => {

                expense.StartDate = new Date(expense.StartDate);
                expense.EndDate = new Date(expense.EndDate);

                if (expense.RejectedComments) {
                    expense.RejectedComments.forEach(comment => {
                        comment.DateRejected = new Date(comment.DateRejected);
                    });
                }

            })

            return values;
        }

    }

    // the query string is an object on the form {key: 'name', value: 'ibrahem'} => ?name=ibrahem
    var postData = function ($http, url, data, query, $mdToast) {

        if (query)
            var query_string = `?${query.key}=${query.value}`;
        else
            var query_string = '';

        //return $http request as a promise object
        return $http.post(url + query_string, JSON.stringify(data))
            .then((response) => { showToast($mdToast, 'Done successfully.'); return response.data; })
            .catch((error) => { showToast($mdToast, "Error. Couldn't complete the operation!"); console.log(error); throw error});
    }

    // the query string is an object on the form {key: 'name', value: 'ibrahem'} => ?name=ibrahem
    var getData = function ($http, url, query) {

        if (query)
            var query_string = `?${query.key}=${query.value}`;
        else
            var query_string = '';

        //return $http request as a promise object
        return $http.get(url + query_string)
            .then((response) => response.data)
            .then((values) => fromStringToDate(values))
            .catch((error) => error);
    }

    var showToast = function ($mdToast, textContent) {
        $mdToast.show(
            $mdToast.simple().textContent(textContent).position('bottom left').hideDelay(3000)
        );
    }

    // it depends on the InvoiceSystemResources service (value)
    invoiceApprovalApp.service('InvoiceSystemExpenses', ['InvoiceSystemResources', '$http', 'InvoiceSystemAccounts', 'InvoiceSystemValues', '$mdToast', Constructor]);

})();