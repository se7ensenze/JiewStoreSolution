angular.module('myApp').controller('newOrderController',
    function ($scope, customerService) {

        $scope.code = '';
        $scope.amount = 0;
        $scope.message = '';
        $scope.codeInfo = '';
        $scope.lastCode = '';

        $scope.addBuyAmount = function () {
            $scope.message = 'requesting';
            customerService.addBuyAmount($scope.code, $scope.amount,
                //success
                function (response) {
                    let result = response.data;
                    if (result.isSuccess) {
                        $scope.message = 'add buy amount successfully';
                    }
                    else {
                        $scope.message = result.message;
                    }
                },
                //failure
                function (response) { });

        }

        $scope.getCodeInfo = function () {
            if (!$scope.code) {
                $scope.codeInfo = '';
                $scope.lastCode = '';
                return;
            }
            else if ($scope.lastCode == $scope.code) {
                return;
            } 
            customerService.getCodeInfo($scope.code,
                //success
                function (response) {
                    let result = response.data;
                    if (result.isSuccess) {
                        let info = result.customer;
                        $scope.lastCode = $scope.code;
                        $scope.codeInfo = info.fullName + ' has ' + info.remainingPoint + ' point remaining';
                    }
                    else {
                        $scope.codeInfo = '';
                        $scope.lastCode = '';
                    }
                },
                //failure
                function (response) { });
        };
    }
);