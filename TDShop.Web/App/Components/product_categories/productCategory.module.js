/// <reference path="../../../assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('tdshop.product_categories', ['tdshop.common']).config(config);

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
})();