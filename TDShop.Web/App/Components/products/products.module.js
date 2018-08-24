/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('tdshop.products', ['tdshop.common']).config(config).config(configAuthentication);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('products', {
                parent:'base',
                url: "/products",
                templateUrl: "/App/Components/products/productListView.html",
                controller: "productListController"
            })
            .state('product_add', {
                parent: 'base',
                url: "/product_add",
                templateUrl: "/App/Components/products/productAddView.html",
                controller: "productAddController"
            }).state('product_edit', {
                parent: 'base',
                url: "/product_edit/:id",
                templateUrl: "/App/Components/products/productEditView.html",
                controller: "productEditController"
            });
    }

    function configAuthentication($httpProvider) {
        $httpProvider.interceptors.push(function ($q, $location) {
            return {
                request: function (config) {

                    return config;
                },
                requestError: function (rejection) {

                    return $q.reject(rejection);
                },
                response: function (response) {
                    if (response.status == "401") {
                        $location.path('/login');
                    }
                    //the same response/modified/or a new one need to be returned.
                    return response;
                },
                responseError: function (rejection) {

                    if (rejection.status == "401") {
                        $location.path('/login');
                    }
                    return $q.reject(rejection);
                }
            };
        });
    }
})();