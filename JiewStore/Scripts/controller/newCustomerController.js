
angular.module('myApp').controller('newCustomerController', 
    function ($scope, customerService) {
        $scope.newCustomer = {
            nickName: '',
            fullName: '',
            facebook: '',
            address: '',
            phone: '',
            birthDate: '',
            amount: 0
        };

        $scope.save = function () {

            let cus = $scope.newCustomer;

            customerService.addNewCustomer(cus.nickName, cus.fullName, cus.facebook,
                cus.address, cus.phone, cus.birthDate, cus.amount,
                //sucess
                function (response) { 
                    cus.nickName = '';
                    cus.fullName = '';
                    cus.facebook = '';
                    cus.address = '';
                    cus.phone = '';
                    cus.birthDate = '';
                    cus.amount = 0;
                },
                //failure
                function (response) {

                })
        };
    }
);