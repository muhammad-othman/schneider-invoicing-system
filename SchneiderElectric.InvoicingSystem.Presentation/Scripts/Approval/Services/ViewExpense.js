// this file contains the angular js service used for displaying the expense details
// it depends on the InvoiceSystemResources service (value)
(function () {

    // the name of the angular js module
    var angularAppName = 'myApp';

    var invoiceApprovalApp = angular.module(angularAppName);

    var Constructor = function ($mdDialog, InvoiceSystemResources, InvoiceSystemAccounts) {

        this.show = (expense) => showExpense(expense, $mdDialog, InvoiceSystemResources);

    }

    // views the expense details in a pop-up mdDialog
    var showExpense = function (expense, $mdDialog, InvoiceSystemResources) {
        console.log(expense.EmployeeName);
        console.log(expense.ProjectName);
        console.log(expense);
        $mdDialog.show({
            // activeExpense can be injected in the controller
            // it represents the epense that was selected
            // its details will be viewed
            locals: { activeExpense: expense },
            controller: ['$scope', 'activeExpense', '$mdDialog', 'InvoiceSystemExpenses', 'InvoiceSystemResources', 'InvoiceSystemAccounts', showExpenseDialogController],
            templateUrl: InvoiceSystemResources.templates.expenseDetailsTemplateUrl,
            parent: angular.element(document.body),
            clickOutsideToClose: true,
            fullscreen: true // Only for -xs, -sm breakpoints.
        });

    }

    var showExpenseDialogController = function ($scope, activeExpense, $mdDialog, InvoiceSystemExpenses, InvoiceSystemResources, InvoiceSystemAccounts) {

        // the local scope for the dialog
        $scope.expense = activeExpense;
       
        // if the account can't cancel or if the expense is already cancelled
        $scope.hideCancelExpenseButton = !InvoiceSystemAccounts.canCancel() || InvoiceSystemExpenses.isCancelled(activeExpense);
        // set the text displayed on the cancelExpense button
        $scope.cancelExpenseButtonText = InvoiceSystemResources.labels.expenseActions.cancel;
        // change the text on the submit button depending on it being a recall (for a canceled expense) or a submit
        $scope.submitExpenseButtonText = InvoiceSystemExpenses.isCancelled(activeExpense) ?
                                             InvoiceSystemResources.labels.expenseActions.recall :
                                                 InvoiceSystemResources.labels.expenseActions.submit;

        $scope.closeDialog = () => { $mdDialog.cancel() };
        $scope.cancelExpense = () => cancelExpense(activeExpense, $mdDialog, InvoiceSystemExpenses);
        $scope.submitExpense = () => submitExpense(activeExpense, $mdDialog, InvoiceSystemExpenses, InvoiceSystemAccounts);


        // self expenses
        $scope.selfExpenses = activeExpense.SelfExpenses;

        // prediem expenses
        $scope.perdiemExpenses = activeExpense.PerdiemExpenses;

        // overtime expenses
        $scope.overTimeExpenses = activeExpense.OverTimeExpenses;
        console.log("$scope.expense");
        console.log($scope.expense);

    }

    var submitExpense = function (expense, $mdDialog, InvoiceSystemExpenses, InvoiceSystemAccounts) {

        var isBeingRecalled = InvoiceSystemExpenses.isCancelled(expense);

        if (!isBeingRecalled)
            var isBeingRejected = InvoiceSystemExpenses.isBeingRejected(expense);

        var action = getActionString(isBeingRejected, isBeingRecalled, InvoiceSystemAccounts);

        var message = `Are you sure you want to ${(action)} this expense?`;

        showConfirm($mdDialog, message, isBeingRejected)
            .then(function (comment) {

                if (isBeingRejected) {
                    // I think the comment (for rejecting an expense) should be obligatory
                    // thus, the ExpenseStateId in the comment table is not needed
                    
                    expense.RejectedComments.push({
                        ExpenseId: expense.ExpenseId,
                        DateRejected: new Date(),
                        Comment: comment
                    });

                    InvoiceSystemExpenses.submitExpense(expense)
                        .then(() => removeFromActiveExpenses(InvoiceSystemExpenses, expense));
                }

                else if (isBeingRecalled)
                    InvoiceSystemExpenses.recallExpense({ key: 'expenseId', value: expense.ExpenseId })
                        .then(() => cancelledToActiveExpenses(InvoiceSystemExpenses, expense));
                else
                    InvoiceSystemExpenses.submitExpense(expense)
                        .then(() => {
                            InvoiceSystemExpenses.isRejected(expense) ? removeFromRejectedExpenses(InvoiceSystemExpenses, expense)
                                : removeFromActiveExpenses(InvoiceSystemExpenses, expense);
                        });
            });
    }

    var getActionString = function (isBeingRejected, isBeingRecalled, InvoiceSystemAccounts) {

        if (isBeingRejected)
            return 'reject';
        else if (isBeingRecalled)
            return 'recall';
        else if (InvoiceSystemAccounts.isAtFinance())
            return 'finish';
        else
            return 'approve';

    }

    var cancelExpense = function (expense, $mdDialog, InvoiceSystemExpenses) {
        var message = 'Are you sure you want to cancel this expense?';
        showConfirm($mdDialog, message).then(() => InvoiceSystemExpenses.cancelExpense({ key: 'expenseId', value: expense.ExpenseId })).
            then(() => activeToCancelledExpenses(InvoiceSystemExpenses, expense));
    }

    var showConfirm = function ($mdDialog, message, getComment = false) {

        var action = getComment ? 'prompt' : 'confirm';

        var confirm = $mdDialog[action]()
            .title('Confirmation')
            .textContent(message)
            .ok('Yes')
            .cancel('Cancel');
        if (getComment)
            confirm.placeholder('Why did you reject this expense?');

        return $mdDialog.show(confirm);
    };

    var activeToCancelledExpenses = function (InvoiceSystemExpenses, expense) {
        // remove from active expenses
        InvoiceSystemExpenses.removeFromActive(expense);
        // set as cancelled
        InvoiceSystemExpenses.setAsCancelled(expense);
        // add to cancelled
        InvoiceSystemExpenses.addToCancelled(expense);
    }

    var cancelledToActiveExpenses = function (InvoiceSystemExpenses, expense) {
        // remove from cancelled expenses
        InvoiceSystemExpenses.removeFromCancelled(expense);
        // set as active
        InvoiceSystemExpenses.setAsActive(expense);
        // add to active
        InvoiceSystemExpenses.addToActive(expense);
    }

    var removeFromActiveExpenses = function (InvoiceSystemExpenses, expense) {
        // remove from active expenses
        InvoiceSystemExpenses.removeFromActive(expense);
    }

    var removeFromRejectedExpenses = function (InvoiceSystemExpenses, expense) {
        // remove from rejected expenses
        InvoiceSystemExpenses.removeFromRejected(expense);
    }

    invoiceApprovalApp.service('ViewExpense', ['$mdDialog', 'InvoiceSystemResources', 'InvoiceSystemAccounts', Constructor]);

})();