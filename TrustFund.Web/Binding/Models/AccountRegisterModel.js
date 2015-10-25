(function (tf) {
    var AccountRegisterModelStep1 = function () {
        var self = this;
        self.FirstName = '';
        self.LastName = '';
        self.Address = '';
        self.city = '';
        self.State = '';
        self.ZipCode = '';

        self.Initialized = false;
    };

    tf.AccountRegisterModelStep1 = AccountRegisterModelStep1;
}(window.TrustFund));

(function (tf) {
    var AccountRegisterModelStep2 = function () {

        var self = this;

        self.LoginEmail = '';
        self.Password = '';
        self.PasswordConfirm = '';

        self.Initialized = false;
    };
    tf.AccountRegisterModelStep2 = AccountRegisterModelStep2;
}(window.TrustFund));

