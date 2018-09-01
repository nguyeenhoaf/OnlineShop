/// <reference path="../../../assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('tdshop.product_categories', ['tdshop.common']).config(config).config(configAuthentication);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider
            .state('product_categories', {
                parent: 'base',
                url: "/product_categories",
                templateUrl: "/App/Components/product_categories/productCatogoryListView.html",
                controller: "productCategoryListController"
            })
            .state('add_product_category', {
                parent: 'base',
                url: "/add_product_category",
                templateUrl: "/App/Components/product_categories/productCategoryAddView.html",
                controller:"productCategoryAddController"
            }).state('edit_product_category', {
                parent: 'base',
                url: "/edit_product_category/:id",
                templateUrl: "/App/Components/product_categories/productCategoryEditView.html",
                controller: "productCategoryEditController"
            })
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