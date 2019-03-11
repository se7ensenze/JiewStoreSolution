angular.module('myApp').controller('parameterController',
    function ($scope, parameterService) {

        $scope.pointPerAmount = 0;
        $scope.tierLevels = [];

        // ---------------------------------------------------------
        //  init() function
        // ---------------------------------------------------------
        $scope.init = function () {
            parameterService.getParameters(
                //success
                function (response) { 
                    console.log(response);
                    $scope.pointPerAmount = response.data.pointPerAmount;
                    $scope.tierLevels = response.data.tierLevels;
                },
                //failure
                function (response) {
                    console.log(response);
                }
            );
        };
    }
);