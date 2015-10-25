//todo: create a new module for customer file upload
//todo: include inputfile.css in css bundle
appMainModule.controller("CustomerFileViewModel", function ($scope, $http,$modal, $location, viewModelHelper, validator) {
    $scope.viewModelHelper = viewModelHelper;
    $scope.customerFileModel = new TrustFund.CustomerFileModel();
    $scope.files = [];

    var getUploadedFiles = function () {
        viewModelHelper.apiGet("api/customer/uploadedFiles", null, function (result) {
            angular.forEach(result.data, function (file) {
                file.UploadDate = new Date(parseInt(file.UploadDate.replace(/\/Date\((\d+)\)\//gi, "$1"))).toLocaleDateString(); //parse date data to readable format.
            });
            $scope.files = result.data;
        });
    }
    getUploadedFiles();

    $scope.animationEnabled = true;
    $scope.open = function () {
        var modalInstance = $modal.open({
            animation: $scope.animationEnabled,
            templateUrl: 'uploadFile.html',
            controller: 'uploadFileModalCtrl',
            size: 'lg',
            resolve: {
                items: function () {
                    return $scope.files;
                }
            }
        });
    };
});

appMainModule.controller('uploadFileModalCtrl', function ($scope, $modalInstance, items) {
    $scope.items = items;
    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
});

//manipulate the upload file template
appMainModule.directive('uploadFile', function () {
    return {
        // Restrict it to be an attribute in this case
        restrict: 'A',
        // responsible for registering DOM listeners as well as updating the DOM
        link: function (scope, element, attrs) {
            $(element).fileinput(scope.$eval(attrs.uploadFile));
        },
        controller: function ($scope, $element, $attrs) {
            $($element).on('fileuploaded', function (event, data, previewId, index) {
                console.log(data);
                var responses = data.response;
                angular.forEach(responses, function (response) {
                    response.UploadDate = new Date(parseInt(file.UploadDate.replace(/\/Date\((\d+)\)\//gi, "$1"))).toLocaleDateString(); //parse date to readable format. 
                    $scope.items.push(response);
                });
                $scope.$apply();
                console.log($scope.items);
            });
        }
    };
});