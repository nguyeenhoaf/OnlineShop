/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('tdshop',
        ['tdshop.products',
            'tdshop.product_categories',
            'tdshop.common'])
        .config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('home', {
            url: "/admin",
            templateUrl: "/App/Components/home/homeView.html",
            controller: "abcController"
        });
        $urlRouterProvider.otherwise('/admin');
    }
})();