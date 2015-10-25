(function (tf) {
    var AccountLoginModel = function () {
        var self = this;
        self.LoginEmail = '';
        self.Password = '';
        self.RememberMe = false;
    }

    tf.AccountLoginModel = AccountLoginModel;
}(window.TrustFund));