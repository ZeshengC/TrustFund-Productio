(function (tf) {
    var CustomerFileModel = function () {
        var self = this;

        self.FileId = '';
        self.FileName = '';
        self.Type = '';
        self.UploadDate='';
        self.AccountId='';
        self.Directory=''
    }

    tf.CustomerFileModel = CustomerFileModel;
}(window.TrustFund));