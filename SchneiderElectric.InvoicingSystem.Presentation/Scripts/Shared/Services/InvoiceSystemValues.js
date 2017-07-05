// this file contains the angular js service used for getting values data from the back-end WebApi from the values controller
// it depends on the InvoiceSystemResources service (value)
(function () {

    // the name of the angular js module
    var angularAppName = 'myApp';

    var invoiceApprovalApp = angular.module(angularAppName);

    var InvoiceSystemValues = function (InvoiceSystemResources, $http) {

        this.getAllValues = getAll($http, InvoiceSystemResources);

    }
    
    var getAll = function ($http, InvoiceSystemResources) {

        // returns a proxy that resolves when all the proxies have resolved
        return Promise.all(
            [
                getData($http, InvoiceSystemResources.values.getCountryList.url, InvoiceSystemResources.values.getCountryList.objectName),
                getData($http, InvoiceSystemResources.values.getCurrencies.url, InvoiceSystemResources.values.getCurrencies.objectName),
                getData($http, InvoiceSystemResources.values.getPerdiemRates.url, InvoiceSystemResources.values.getPerdiemRates.objectName),
                getData($http, InvoiceSystemResources.values.getExpenseTypes.url, InvoiceSystemResources.values.getExpenseTypes.objectName),
                getData($http, InvoiceSystemResources.values.getExpenseCategories.url, InvoiceSystemResources.values.getExpenseCategories.objectName),
                getData($http, InvoiceSystemResources.values.getExpenseStates.url, InvoiceSystemResources.values.getExpenseStates.objectName)
            ]
        ).then((array) => convertArrayToObject(array));

    }

    var getData = function ($http, url, key) {

        //return $http request as a promise object
        return $http.get(url).then((response) => _wrapObject(response.data, key)).catch((error) => error);

    }

    // this function is used to wrap an object inside another object that has two properties named as key, value
    var _wrapObject = function (obj, key) {

        var newObject = {};
        newObject.key = key;
        newObject.value = obj;
        return newObject;

    }

    // this function is used for c onverting the output of the Promise.all function (which is an array of objects)
    // to an object with named properties for convenience.
    // it depends on the array to have a "key" propery in each of it's elements to be used as the name of the object
    // so [{key: "exports", value: 'rice'},{key: "imports", value: 'wheat'}] will be transformed into {exports: 'rice', imports: 'wheat'}
    var convertArrayToObject = function (array) {

        var obj = {};

        for (var i of array) {
            obj[i.key] = i.value;
        }
        
        return obj;

    }

    // it depends on the InvoiceSystemResources service (value)
    invoiceApprovalApp.service('InvoiceSystemValues', ['InvoiceSystemResources', '$http', InvoiceSystemValues]);

})();