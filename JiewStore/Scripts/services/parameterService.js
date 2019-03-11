angular.module('myApp')
    .service('parameterService', function ($http) {
        this.getParameters = function (successFn, failureFn) {
            $http({
                url: "parameter/getParameters",
                dataType: 'json',
                method: 'POST',
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(successFn, failureFn);
        }
    });