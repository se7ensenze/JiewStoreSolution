
angular.module("myApp", ['ngMaterial', 'ui.router'])
    .config(function ($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.otherwise('/home');
        $stateProvider.state('home', {
            url: '/home',
            templateUrl: 'pages/home.html'
        });
        $stateProvider.state('customers', {
            url: '/customers',
            templateUrl: 'pages/customers.html'
        });
        $stateProvider.state('newCustomer', {
            url: '/newCustomer',
            templateUrl: 'pages/newCustomer.html'
        });
        $stateProvider.state('newOrder', {
            url: '/newOrder',
            templateUrl: 'pages/newOrder.html'
        });
        $stateProvider.state('redeem', {
            url: '/redeem',
            templateUrl: 'pages/redeem.html'
        });
        $stateProvider.state('parameters', {
            url: '/parameters',
            templateUrl: 'pages/parameters.html'
        });
    });

