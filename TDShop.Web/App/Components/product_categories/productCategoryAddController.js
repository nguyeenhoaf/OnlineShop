/// <reference path="../../../assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('productCategoryAddController', productCategoryAddController);
    productCategoryAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];
    function productCategoryAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true,
            Name: ""
        }
       
        $scope.AddProductCategory = AddProductCategory;
        $scope.$watch('productCategory.Name', function () {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        });
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