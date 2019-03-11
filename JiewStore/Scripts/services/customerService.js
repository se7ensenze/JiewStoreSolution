
angular.module('myApp')
    .service('customerService', function ($http) {

        /**
         * getCustomers function
         */
        this.getCustomers = function (pageNo, pageSize, successFn, failedFn) {
            $http({
                url: "customers/getCustomers",
                dataType: 'json',
                method: 'POST',
                data: {
                    pageNo: pageNo,
                    pageSize: pageSize
                },
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(successFn, failedFn);
        };

        /*
         * addNewCustomer function
         */
        this.addNewCustomer = function (nickName, fullName, facebook,
            address, phone, birthDate, amount, successFn, failedFn) {
            $http({
                url: "customers/addNewCustomer",
                dataType: 'json',
                method: 'POST',
                data: {
                    nickName: nickName,
                    fullName: fullName,
                    facebook: facebook,
                    address: address,
                    birthDate: birthDate.toLocaleDateString('en'),
                    phone: phone,
                    amount: amount
                },
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(successFn, failedFn);
        };

        /*
         redeem function
         */
        this.redeem = function (code, amount, successFn, failedFn) {
            $http({
                url: "customers/redeem",
                dataType: 'json',
                method: 'POST',
                data: {
                    code: code,
                    amount: amount,
                },
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(successFn, failedFn);
        };
        /*
         addBuyAmount function
         */
        this.addBuyAmount = function (code, amount, successFn, failedFn) {
            $http({
                url: "customers/addBuyAmount",
                dataType: 'json',
                method: 'POST',
                data: {
                    code: code,
                    amount: amount,
                },
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(successFn, failedFn);
        };
        /*
         getCodeInfo function
         */
        this.getCodeInfo = function (code, successFn, failedFn) {
            console.log('getCodeInfo :=', code);
            $http({
                url: "customers/getCustomerInfo",
                dataType: 'json',
                method: 'POST',
                data: {
                    code: code,
                    includeRemainingPoint: true
                },
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(successFn, failedFn);
        };
    });