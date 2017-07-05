// custom directive that displays self expenses items of the expense
(function () {

    // the name of the angular js module
    var angularAppName = 'myApp';

    var invoiceApprovalApp = angular.module(angularAppName);

    var disableEditing = function () {
        return {
            // can only be used as an attribute
            restrict: 'A',
            // empty isolated scope
            scope: {
                disableEditing: '='
            },
            controller: ['$scope', '$compile', '$element', controller],
            link: link
        }
    }

    var controller = function ($scope, $compile, $element) {

        $scope.disable = function (original, copy) {

            //copy.attr('ng-disabled', 'true');
            //compiledElements = $compile(copy)($scope);
            original.replaceWith(copy);

        }

    }

    var link = function ($scope, element, attributes, controller) {

        var selector = 'md-select,input,md-datepicker,button,select,md-option,textarea';
        var selector_checkBox = 'md-checkbox';

        if ($scope.disableEditing) {

            $('document').ready(function () {
                $(element).find(selector).prop('disabled', true).attr('disabled', true);
                var original = $(element).find(selector_checkBox);
                var copy = original.clone();
                $scope.disable(original, copy);
            });

        }
    }

    invoiceApprovalApp.directive('disableEditing', [disableEditing]);

})();