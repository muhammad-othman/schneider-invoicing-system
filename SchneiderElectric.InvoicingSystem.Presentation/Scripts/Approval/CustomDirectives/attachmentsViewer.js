// custom directive that displays overtime expense item of the expense
(function () {

    // the name of the angular js module
    var angularAppName = 'myApp';

    var invoiceApprovalApp = angular.module(angularAppName);

    var attachmentsViewer = function (InvoiceSystemResources) {
        return {
            // can only be used as an element
            restrict: 'E',
            // isolated scope
            scope: {
                expense: '='
            },
            templateUrl: InvoiceSystemResources.templates.attachmentsViewerUrl,
            controller: ['$scope', 'InvoiceSystemResources', controller]
        }
    }

    var controller = function ($scope, InvoiceSystemResources) {

        $scope.hasAttachments = $scope.expense.Files.length > 0;

        if ($scope.hasAttachments)
            $scope.files = $scope.expense.Files;

        $scope.downloadFile = (fileId) => downloadFile(fileId, InvoiceSystemResources);

        $scope.errorMessage = InvoiceSystemResources.messages.dataLoading.empty;
    }

    var downloadFile = function (fileId, InvoiceSystemResources) {

        var downloadUrl = `${InvoiceSystemResources.expenses.get.fileDownloadUrl}?FileId=${fileId}`;
        //window.open(downloadUrl, '_blank', '');  
        window.location.href = downloadUrl;

    }

    invoiceApprovalApp.directive('attachmentsViewer', ['InvoiceSystemResources', attachmentsViewer]);

})();