(function (app) {
    app.controller('productListController', productListController);

    function productListController() {
        console.log("Hello form product");
    }
})(angular.module('tdshop.products'));