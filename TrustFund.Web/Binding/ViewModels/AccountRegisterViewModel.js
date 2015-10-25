var accountRegisterModule = angular.module('accountRegister', ['common'])
    .config(function ($routeProvider, $locationProvider) {
        $routeProvider.when('/step1', { templateUrl: TrustFund.rootPath + 'Templates/RegisterStep1.html', controller: 'AccountRegisterStep1ViewModel' });
        $routeProvider.when('/step2', { templateUrl: TrustFund.rootPath + 'Templates/RegisterStep2.html', controller: 'AccountRegisterStep2ViewModel' });
        $routeProvider.when('/confirm', { templateUrl: TrustFund.rootPath + 'Templates/RegisterConfirm.html', controller: 'AccountRegisterConfirmViewModel' });
        $routeProvider.otherwise({ redirectTo: '/step1' });
        $locationProvider.html5Mode({
            enabled: true
        });
    });

accountRegisterModule.controller("AccountRegisterViewModel", ['$scope','$http','$location','$window','viewModelHelper', function ($scope, $http, $location, $window, viewModelHelper) {
    $scope.viewModelHelper = viewModelHelper;

    $scope.accountModelStep1 = new TrustFund.AccountRegisterModelStep1();
    $scope.accountModelStep2 = new TrustFund.AccountRegisterModelStep2();

    $scope.previous = function () {
        $window.history.back();
    }
}]);

accountRegisterModule.controller("AccountRegisterStep1ViewModel", function ($scope, $http, $location, viewModelHelper, validator) {
    viewModelHelper.modelIsValid = true;
    viewModelHelper.modelErrors = [];

    var accountModelStep1Rules = [];

    var setupRules = function () {
        accountModelStep1Rules.push(new validator.PropertyRules("FirstName", {
            required: { message: "First name is required" }
        }));

        accountModelStep1Rules.push(new validator.PropertyRules("LastName", {
            required: { message: "Last name is required" }
        }));

        accountModelStep1Rules.push(new validator.PropertyRules("Address", {
            required: { message: "Address is required" }
        }));

        accountModelStep1Rules.push(new validator.PropertyRules("City", {
            required: { message: "City is required" }
        }));

        accountModelStep1Rules.push(new validator.PropertyRules("State", {
            required: { message: "State" }
        }))
        accountModelStep1Rules.push(new validator.PropertyRules("ZipCode", {
            required: { message: "Zip code is required" },
            pattern: { message: "Zip code is in invalid format", params: /^[0-9]{5}(?:-[0-9]{4})?$/ }
        }));
    };

    $scope.step2 = function () {
        validator.ValidateModel($scope.accountModelStep1, accountModelStep1Rules);
        viewModelHelper.modelIsValid = $scope.accountModelStep1.isValid;
        viewModelHelper.modelErrors = $scope.accountModelStep1.errors;

        if (viewModelHelper.modelIsValid) {
            viewModelHelper.apiPost('api/account/register/validate1', $scope.accountModelStep1, function (result) {
                $scope.accountModelStep1.Initialized = true;
                $location.path('/step2');
            });
        }
        else {
            viewModelHelper.modelErrors = $scope.accountModelStep1.errors;
        }
    }
    setupRules();
});

accountRegisterModule.controller("AccountRegisterStep2ViewModel", function ($scope, $http, $location, $window, viewModelHelper, validator) {
    if (!$scope.accountModelStep1.Initialized) {
        $location.path('/step1');
    }

    viewModelHelper.modelIsValid = true;
    viewModelHelper.modeErrors = [];

    var accountModelStep2Rules = [];
    var setupRules = function () {
        accountModelStep2Rules.push(new validator.PropertyRules("LoginEmail", {
            required: { message: "Login Email is required" },
            email: { message: "Login emial is not an email" }
        }));
        accountModelStep2Rules.push(new validator.PropertyRules("Password", {
            required: { message: "Password is required" },
            minLength: { message: "Password must be at least 6 characters", params: 6 }
        }));
        accountModelStep2Rules.push(new validator.PropertyRules("PasswordConfirm", {
            required: { message: "Password confirmation is required" },
            custom: {
                validator: TrustFund.mustEqual,
                message: "Password do not match",
                params: function () { return $scope.accountModelStep2.Password; }
            }
        }));
    };
    $scope.confirm = function () {
        validator.ValidateModel($scope.accountModelStep2, accountModelStep2Rules);
        viewModelHelper.modelIsValid = $scope.accountModelStep2.isValid;
        viewModelHelper.modelErrors = $scope.accountModelStep2.errors;
        if (viewModelHelper.modelIsValid) {
            viewModelHelper.apiPost('api/account/register/validate2', $scope.accountModelStep2, function (result) {
                console.log(result);
                $scope.accountModelStep2.Initialized = true;
                $location.path('/confirm');
            });
        }
    }
    setupRules();
});



accountRegisterModule.controller("AccountRegisterConfirmViewModel", function ($scope, $http, $location, $window, viewModelHelper, validator) {
    if (!$scope.accountModelStep2.Initialized) {
        $location.path('/step1');
    }
    console.log($scope.accountModelStep2.LoginEmail);

    $scope.createAccount = function () {
        var accountModel;

        accountModel = $.extend(accountModel, $scope.accountModelStep1);
        accountModel = $.extend(accountModel, $scope.accountModelStep2);


        console.log(accountModel);
        viewModelHelper.apiPost('api/account/register', accountModel, function (result) {
            $window.location.href = TrustFund.rootPath;
        });
    }
});