// this file contains the function that creates an angular js value service to hold
// data and functions to handle account types (ProjectAdmin, EM, Finance, ...)
(function () {

    // the name of the angular js module
    var angularAppName = 'myApp';

    var invoiceApprovalApp = angular.module(angularAppName);

    var Constructor = function (InvoiceSystemResources){

        types = InvoiceSystemResources.AccountTypes;

        this.isAtProjectAdmin = isAtProjectAdmin;

        this.isAtFinance = isAtFinance;

        this.getTypeFromString = getTypeFromString;

        this.setType = setType;

        this.getDisplayName = getDisplayName;

        this.canReject = canReject;

        this.canCancel = canCancel;

        this.hasRejectedExpenses = hasRejectedExpenses;

        this.hasCancelledExpenses = hasCancelledExpenses;

        this.canViewProjectNames = canViewProjectNames;

    };

    var types = {};

    // this property is used to store the type
    // default value is PA.
    var thisAccount = {
        type: types.ProjectAdmin
    };

    var isAtProjectAdmin = function () {

        if (thisAccount.type == types.ProjectAdmin)
            return true;
        else
            return false;

    };

    var isAtFinance = function () {

        if (thisAccount.type == types.Finance)
            return true;
        else
            return false;

    }

    var getTypeFromString = function (typeName) {

        typeName = typeName.toLowerCase();

        if (typeName.match(/atem|engineeringmanager|em/))
            return types.EngineeringManager;
        else if (typeName.match(/finance/))
            return types.Finance;
        else
            return types.ProjectAdmin;
    }

    // this is used to execute the function only once
    var hasBeenSet = false;

    var setType = function (type) {

        if (!hasBeenSet) {
            hasBeenSet = true;
            thisAccount.type = getTypeFromString(type);
        }

    }

    var getDisplayName = function () {
        if (thisAccount.type == types.ProjectAdmin)
            return "Project Administrator";
        else if (thisAccount.type == types.EngineeringManager)
            return "Engineering Manager";
        else if (thisAccount.type == types.Finance)
            return "Finance";
    }

    var canReject = function () {
        return !isAtProjectAdmin();
    }

    var canCancel = function () {
        return isAtProjectAdmin();
    }

    var hasCancelledExpenses = function () {
        return isAtProjectAdmin();
    }

    var hasRejectedExpenses = function () {
        return isAtProjectAdmin();
    }

    var canViewProjectNames = function () {
        return !isAtProjectAdmin();
    }

    invoiceApprovalApp.service('InvoiceSystemAccounts', ['InvoiceSystemResources', Constructor]);

})();