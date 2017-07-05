var myApp = angular.module('myApp', ['ngMaterial', 'ngMessages']);

myApp.config(function ($mdThemingProvider) {
    $mdThemingProvider.theme('default').primaryPalette('green').accentPalette('light-green');
});

myApp.directive('uploadFiles', ['$parse', uploadFiles]);

function uploadFiles($parse) {
    var directive = {
        restrict: 'E',
        template: '<input id="fileInput" type="file" multiple class="ng-hide">\
                            <div>\
                                <md-button id="uploadButton"\
                                    class="md-raised md-primary"\
                                    aria-label="attach_file">\
                                        Attach new file...\
                                </md-button>\
                            </div>\
                    </div >',
        link: (scope, element, attributes, controller) => uploadFilesLink(scope, element, attributes, controller, $parse)
    };
    return directive;
}

var guid = '2c82a84e-26dd-4bd2-89b7-002054269c18';

function uploadFilesLink(scope, element, attrs, controller, $parse) {
    var input = $(element[0].querySelector('#fileInput'));
    var button = $(element[0].querySelector('#uploadButton'));
    var parentDiv = input.parent().parent();
    globale = input;

    if (input.length && button.length) {
        button.click(function (e) {
            input.click();
        });
    }

    var onChange = $parse(attrs.callFunction);
    input.on("change", function (event) {
        onChange(scope, { $files: event.target.files });
    });
}

myApp.directive("ngFiles", ["$parse", function ($parse) {
    function fn_link(scope, element, attrs) {
        var onChange = $parse(attrs.ngFiles);
        element.on("change", function (event) {
            onChange(scope, { $files: event.target.files });
        });
    };
    return {
        link: fn_link
    };
}]);

myApp.factory("api", ["$http", "$q", api]);

function api($http, $q) {
    var dataService = $http;

    function saveExpense(expense) {
        var deffered = $q.defer();
        //expense.OverTimeExpenses[0].OverTimeExpenseId = guid;
        //expense.Files = [];
        dataService.post("/api/expense/create/", JSON.stringify(expense))
            .then(function (data) {
                deffered.resolve(data);
            }, function () {
                deffered.reject();
            });
        return deffered.promise;
    };


    function postExpense(expense) {
        var deffered = $q.defer();
        console.log(expense);
        dataService.post("/api/expense/submit/", JSON.stringify(expense))
            .then(function (data) {
                deffered.resolve(data);
            }, function () {
                deffered.reject();
            });
        return deffered.promise;
    };

    

    function getCurrencies() {
        var deffered = $q.defer();
        dataService.get("/api/values/getCurrencies/")
            .then(function (data) {
                deffered.resolve(data);
            }, function () {
                deffered.reject();
            });
        return deffered.promise;
    };

    function getCategories() {
        var deffered = $q.defer();
        dataService.get("/api/values/getExpenseCategories/")
            .then(function (data) {
                deffered.resolve(data);
            }, function () {
                deffered.reject();
            });
        return deffered.promise;
    };

    function getSavedExpenses() {
        var deffered = $q.defer();
        dataService.get("/api/expense/getall/")
            .then(function (data) {
                deffered.resolve(data);
            }, function () {
                deffered.reject();
            });
        return deffered.promise;
    };
    

    function getCountries() {
        var deffered = $q.defer();
        dataService.get("/api/values/getCountries/")
            .then(function (data) {
                deffered.resolve(data);
            }, function () {
                deffered.reject();
            });
        return deffered.promise;
    };

    function getRates() {
        var deffered = $q.defer();
        dataService.get("/api/values/getRates/")
            .then(function (data) {
                deffered.resolve(data);
            }, function () {
                deffered.reject();
            });
        return deffered.promise;
    };

    function editExpense(expenseId) {
        var deffered = $q.defer();
        dataService.get("/api/expense/getByID?id=" + expenseId)
            .then(function (data) {
                deffered.resolve(data);
            }, function () {
                deffered.reject();
            });
        return deffered.promise;
    };

    return {
        postExpense: postExpense,
        saveExpense: saveExpense,
        editExpense: editExpense,
        getSavedExpenses: getSavedExpenses,
        getCurrencies: getCurrencies,
        getCategories: getCategories,
        getCountries: getCountries,
        getRates: getRates
    };
};

myApp.controller('engineerController', function ($scope, $mdDialog, $mdSidenav, $mdToast, api, $http) {
    //$scope.id = $routeParams.id;

    function generateGuid() {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c === 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    }

    //var expenseId = generateGuid();
    $scope.expense = {
        "OverTimeExpenses": [
            {
                //"OverTimeExpenseId": generateGuid(),
                "Description": "",
                "ExpenseCategory": "1",
                "Rejected": false,
                "ExpenseId": ""
            }],
        "PerdiemExpenses": [
            {
                //"PerdiemExpenseId": generateGuid(),
                "Description": "",
                "RateId": "",
                "Rejected": false,
                "ExpenseId": ""
            }],
        "SelfExpenses": [],
        "ExpenseId": "",
        "ExpenseDescription": "",
        "EmployeeSapId": "",
        "ProjectSapId": "",
        "StartDate": "",
        "EndDate": "",
        "CountryListID": "",
        "ExpenseState": ""
    };

    formdata = new FormData();

    $scope.getTheFiles = function ($files) {
        
        $scope.attachments = [];

        for (var i = 0; i < $files.length; i++) {
            var reader = new FileReader();
            reader.fileName = $files[i].name;

            reader.onload = function (event) {
                var attachment = {};
                attachment.Name = event.target.fileName;
                attachment.Size = (event.total / 1024).toFixed(2);
                attachment.Src = event.target.result;

                $scope.attachments.push(attachment);
                $scope.$apply();
            }
            reader.readAsDataURL($files[i]);
        }

        angular.forEach($files, function (value, key) {
            formdata.append(guid, value);
            
            globale = $files;
        });
    }

    function uploadFiles(/*expenseId*/) {
        //var Indata = { expense_Id: expenseId } 
        var request = {
            method: "POST",
            url: "/api/files/upload",
            data: formdata,
            headers: {
                'Accept': 'application/octet-stream',
                "Content-Type": undefined
            }//,
            // params: Indata
        };

        $http(request).success(function (d) {
            //alert(d);
            SaveExpense();
        }).error(function (error) {
            console.log(error);
            alert("Nothing uploaded!");
        })
    }

    $scope.reset = function () {
        angular.forEach(
            angular.element(document.querySelectorAll("input [type = 'file']")),
            function (inputElem) {
                angular.element(inputElem).val(null);
            }
        );

        $scope.attachments = [];
        formdata = new FormData();
    }


    $scope.currencies = [];
    $scope.categories = [];
    $scope.countries = [];
    $scope.rates = [];

    api.getCurrencies()
        .then(function (result) {
            $scope.currencies = result.data;
        }, function (error) {
            console.log("cannot get currencies");
        });

    api.getCategories()
        .then(function (result) {
            $scope.categories = result.data;
        }, function (error) {
            console.log("cannot get categories");
        });

    api.getCountries()
        .then(function (result) {
            $scope.countries = result.data;
        }, function (error) {
            console.log("cannot get countries");
        });

    api.getRates()
        .then(function (result) {
            $scope.rates = result.data;
        }, function (error) {
            console.log("cannot get rates");
        });



    var selfExpenseTypeNames = ['Accomodation', 'Transportation', 'Medical'];


    $scope.addSelfExpense = function (type) {

        $scope.expense.SelfExpenses.push({
            "Amount": "",
            "Description": "",
            "CurrencyID": "",
            "ByAmex": "",
            "ExpenseType": type == selfExpenseTypeNames[0] ? 0 : type == selfExpenseTypeNames[1] ? 1 : type == selfExpenseTypeNames[2] ? 2 : 3,
            "RateToUS": "",
            "Rejected": false,
            "ExpenseId": ""
        });

        $scope.accomodationExpenses = $scope.expense.SelfExpenses.filter(selfExpense => selfExpense.ExpenseType == 0);
        $scope.transportationExpenses = $scope.expense.SelfExpenses.filter(selfExpense => selfExpense.ExpenseType == 1);
        $scope.medicalExpenses = $scope.expense.SelfExpenses.filter(selfExpense => selfExpense.ExpenseType == 2);
        $scope.otherExpenses = $scope.expense.SelfExpenses.filter(selfExpense => selfExpense.ExpenseType == 3);
    }

    //$scope.ClearSelfExpense = function (selfExpensesArr, index) {
    //    selfExpensesArr.splice(index, 1);
    //}

    $scope.getFileDetails = function (e) {
        $scope.fileName = e.files[0].name;
    };

    $scope.files = [];

    function activeExpenseController($scope, activeExpense) {
        $scope.expenses = activeExpense;
    }

    // Saleh's controller objects
    var vm = this;

    function toggleMenu() {
        $mdSidenav('menu').toggle();
    };

    $scope.engineer = {};

    $scope.savedExpenses = [];

    api.getSavedExpenses()
        .then(function (result) {
            // data not binded to expenseDetails!
            $scope.savedExpenses = result.data.filter(e=> e.ExpenseState == 1);
        }, function (error) {
            console.log("cannot get saved expenses");
        });


    function EditExpense(expenseId) {
        api.editExpense(expenseId)
        .then(function (response) {
            $scope.expense = response.data;

            $mdToast.show(
                 $mdToast.simple()
                 .textContent('Wait till your data is populated')
                 .action('OK')
                 .hideDelay(3000)
             );
            
        }, function (error) {
            console.log("error");
        });
    }

    function SaveExpense() {
        api.saveExpense($scope.expense)
            .then(function (response) {
                //uploadFiles(response.data);
            }, function (error) {
                console.log("error");
            });
    };

    function Save(savedExpense) {
        $scope.expense.ExpenseState = 1;
        $scope.savedExpenses.push($scope.expense);
        $scope.expense.Files = [];
        for (var pair of formdata.entries()) {
            $scope.expense.Files.push({name: pair[1].name})
        }
        //SaveExpense();
        uploadFiles();
        $mdDialog.show(
            $mdDialog.alert()
            .parent(angular.element(document.querySelector('#Container')))
            .clickOutsideToClose(true)
            .title('')
            .textContent('Expense saved successfully')
            .ariaLabel('')
            .ok('Ok!')
            .targetEvent(savedExpense)
        );
    };


    function submitExpense(expense) {
        api.postExpense(expense)
            .then(function (result) {
                console.log("submited");
                var index = $scope.savedExpenses.findIndex(e=> e.ExpenseId == expense.ExpenseId);
               $scope.savedExpenses.splice(index, 1);
            }, function () {
                console.log("error");
            });
    };

    function Submit(expense) {
        var confirm = $mdDialog.confirm()
            .title('Are you sure to submit the expense?')
            .textContent('you cannot edit it after that')
            .ariaLabel('')
            .targetEvent(event)
            .ok('Yes')
            .cancel('No');
        $mdDialog.show(confirm).then(function () {
            $mdToast.show(
                $mdToast.simple()
                .textContent('Expense submitted successfully')
                .action('OK')
                .hideDelay(3000)
            );
            submitExpense(expense);
        }, function () {
            $mdToast.show(
                $mdToast.simple()
                .textContent('You decided to keep your expense')
                .action('OK')
                .hideDelay(3000)
            );
        });
    };

    // events
    $scope.Save = Save;

    $scope.Submit = Submit;
    $scope.EditExpense = EditExpense;
    $scope.toggleMenu = toggleMenu;
});
