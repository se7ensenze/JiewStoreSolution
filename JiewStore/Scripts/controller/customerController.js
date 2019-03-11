
angular.module('myApp')
    .controller("customersController", function ($scope, $mdSidenav, customerService) {

        $scope.customers = [];
        $scope.init = function () {
            customerService.getCustomers(1, 50,
                function (response) {
                    $scope.customers = response.data;
                },
                function (response) {
                    
                }
            );

        };

        $scope.toggleSidenav = buildToggler('sideNavId');

        function buildToggler(componentId) {
            return function () {
                $mdSidenav(componentId).toggle();
            };
        };

        $scope.Search = function () {
            $scope.toggleSidenav();
        }

    });