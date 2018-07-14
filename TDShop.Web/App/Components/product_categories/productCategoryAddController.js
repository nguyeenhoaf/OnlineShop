/// <reference path="../../../assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('productCategoryAddController', productCategoryAddController);
    productCategoryAddController.$inject = ['apiService', '$scope', 'notificationService', '$state'];
    function productCategoryAddController(apiService, $scope, notificationService, $state) {
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true,
            Name: "Danh mục 1"
        }

        $scope.AddProductCategory = AddProductCategory;

        function AddProductCategory() {
            
            apiService.post('/Api/ProductCategory/create', $scope.productCategory,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                    $state.go('product_categories');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
        }
        function loadParentCategory() {
            apiService.get('/Api/ProductCategory/getallparents', null, function (result) {
                    $scope.parentCategories = result.data;              
            }, function () {
                console.log('Cannot get list parent');
            });
        }
        
        loadParentCategory();
    }
})(angular.module('tdshop.product_categories'))