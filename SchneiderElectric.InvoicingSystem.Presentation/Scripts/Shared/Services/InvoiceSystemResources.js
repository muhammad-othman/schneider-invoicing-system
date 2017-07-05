// this file contains the function that creates an angular js value service to hold
// list of strings and urls for the service to keep them in one place
(function () {

    // the name of the angular js module
    var angularAppName = 'myApp';

    var invoiceApprovalApp = angular.module(angularAppName);
    
    var InvoiceSystemResources = Object.freeze({

        // this object is used to fill data in the ExpenseStates object in the InvoiceSystemExpenses service
        ExpenseStates: {

            // IDs are used for comparisons, these are the only values that should never change
            saved: { key: '1', value: 'AtEmployeeSaved', displayValue: 'Draft' },
            atPA_Active: { key: '2', value: 'AtPA', displayValue: 'At Project Administrator' },
            rejected: { key: '3', value: 'AtPARejected', displayValue: 'Rejected' },
            cancelled: { key: '4', value: 'AtPACanceled', displayValue: 'Cancelled' },
            atEM: { key: '5', value: 'AtEM', displayValue: 'At Engineering Manager' },
            atFinance: { key: '6', value: 'AtFinance', displayValue: 'At Finance' },
            finished: { key: '7', value: 'Finished', displayValue: 'Finished' }

        },

        AccountTypes: {
            ProjectAdmin: 'PA',
            EngineeringManager: 'EM',
            Finance: 'Finance'
        },

        // this object (values) contains urls (WebApi) for (GET)ting data
        values: {
            getCountryList: { url: '/api/values/getCountries', objectName: 'countryList' },
            getCurrencies: { url: '/api/values/getCurrencies', objectName: 'currencies' },
            // e.g; USD Rate 1, USD Rate 2, ...
            getPerdiemRates: { url: '/api/values/getRates', objectName: 'perdiemRates'},
            // e.g; SelfExpense, Medical, ...
            getExpenseTypes: { url: '/api/values/getExpenseTypes', objectName: 'expenseTypes'},
            // this should be (overtime expense categories) like weekday, weekend, ...
            getExpenseCategories: { url: '/api/values/getExpenseCategories', objectName: 'overTimeExpenseCategories' },
            // [(at)Engineer, (at)ProjectAdministrator, Cancelled, Rejected, (at)EM-EngineeringManager, (at)Finance]
            getExpenseStates: { url: '/api/values/getExpenseStates', objectName: 'expenseStates' }
        },

        // this object (expenses) contains urls (WebApi) for (GET)ting and (POST)ing expenses
        expenses: {
            get: {
                // returns expenses pending or action (newly coming or rejected for the project admin, newly coming only for EM and Finance)
                activeUrl: '/api/expense',
                // the expenses that the Project Admin has cancelled
                cancelledUrl: '/api/expense/canceled',
                // the expenses that have been rejected by either the EM or Finance for the Project Admin to reconsider
                rejectedUrl: '/api/expense/rejected',
                // all the expenses that are relevant to the account type
                // useful for monitoring expense states
                allUrl: '/api/expense/all',
                // the url for getting all the files for a certain expense
                //filesUrl: 'api/files/getfiles',
                // the url used to download a certain file
                fileDownloadUrl: '/api/files/download'
            },
            post: {
                // submit an expense
                submitUrl: '/api/expense/submit',
                // cancel an expense
                cancelUrl: '/api/expense/cancel',
                // recall a cancelled expense
                recallUrl: '/api/expense/recall'
            }
        },

        // this object conatains messages shown to the user in case of web requests failure or success
        messages: {

            dataLoading: {
                successful: 'Data was loaded successfully',
                failure: "Data couldn't be loaded",
                empty: "There are no items!"
            },

            submit: {
                successful: 'The expense was submitted successfully',
                failure: "The expense couldn't be submitted"
            },

            cancel: {
                successful: 'The expense was cancelled successfully',
                failure: "The expense couldn't be cancelled"
            },

            recall: {
                successful: 'The expense was recalled successfully',
                failure: "The expense couldn't be recalled"
            },

            reject: {
                successful: 'The expense was submitted successfully as rejected',
                failure: "The expense couldn't be rejected"
            }
        },

        // this object contains HTML labels and titles for buttons, hyper-links, ...
        labels: {
            expenseActions: {
                cancel: 'Set this expense as cancelled',
                submit: 'Submit',
                recall: 'Recall',
                reject: 'Reject'
            }
        },

        // this object contains urls for HTML templates used for dialogs or custom directives
        templates: {
            expensesTableUrl: '/Content/Approval/CustomDirectiveTemplates/expensesTable.html',
            accountTabsUrl: '/Content/Approval/CustomDirectiveTemplates/accountTabs.html',
            selfExpenseUrl: '/Content/Approval/CustomDirectiveTemplates/selfExpense.html',
            expenseDetailsTemplateUrl: '/Content/Approval/expenseDetailsTemplate.html',
            perdiemExpenseUrl: '/Content/Approval/CustomDirectiveTemplates/perdiemExpense.html',
            overTimeExpenseUrl: '/Content/Approval/CustomDirectiveTemplates/overTimeExpense.html',
            expenseInformationUrl: '/Content/Approval/CustomDirectiveTemplates/expenseInformation.html',
            expenseItemsViewerUrl: '/Content/Approval/CustomDirectiveTemplates/expenseItemsViewer.html',
            attachmentsViewerUrl: '/Content/Approval/CustomDirectiveTemplates/attachmentsViewer.html'
        }
    });

    invoiceApprovalApp.value('InvoiceSystemResources', InvoiceSystemResources);

})();