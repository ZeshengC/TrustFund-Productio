appMainModule.controller("AccountLoginViewModel", function ($scope, $http, $location, viewModelHelper, validator) {
    $scope.viewModelHelper = viewModelHelper;
    $scope.accountModel = new TrustFund.AccountLoginModel();
    $scope.returnUrl = '';
    var accountModelRules = [];
    console.log("login");
    var setupRules = function () {
        accountModelRules.push(new validator.PropertyRules("LoginEmail", {
            required: { message: "Login is required" }
        }));

        accountModelRules.push(new validator.PropertyRules("Password", {
            required: { message: "Password is required" },
            minLength: { message: "Password must be at least 6 characters", params: 6 }
        }));
    };

    $scope.login = function () {
        validator.ValidateModel($scope.accountModel, accountModelRules);
        viewModelHelper.modelIsValid = $scope.accountModel.isValid;
        viewModelHelper.modelErrors = $scope.accountModel.errors;
        if (viewModelHelper.modelIsValid) {
            viewModelHelper.apiPost('api/account/login', $scope.accountModel,
                function (result) {
                    if ($scope.returnUrl != '' && $scope.returnUrl.length > 1) {
                        window.location.href = TrustFund.rootPath + $scope.returnUrl.substring(1);
                    }
                    else {
                        window.location.href = TrustFund.rootPath;
                    }
                });
        }
        else {
            viewModelHelper.modelErrors = $scope.accountModel.errors;
        }
    }

    setupRules();

});