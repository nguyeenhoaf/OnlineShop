/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('tdshop.products', ['tdshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('products', {
            url: "/products",
            templateUrl: "/App/Components/products/productListView.html",
            controller: "productListController"
        }).state('product_add', {
            url: "/product_add",
            templateUrl: "/App/Components/products/productAddView.html",
            controller: "productAddController"
        });
    }
})();