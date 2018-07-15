(function (app) {
    app.controller('productCategoryEditController', productCategoryEditController);
    productCategoryEditController.$inject = ['$scope', '$state', '$stateParams', 'apiService', 'notificationService', 'commonService'];
    function productCategoryEditController($scope, $state, $stateParams, apiService, notificationService, commonService) {
        $scope.productCategory = {
            ID: $stateParams.ID,
            CreatedDate: new Date(),
            Status: true
        }
        $scope.UpdateProductCategory = UpdateProductCategory;
        $scope.$watch('productCategory.Name', function () {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        });
        function UpdateProductCategory() {
            apiService.put("/Api/ProductCategory/update", $scope.productCategory,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' cập nhật thành công');
                    $state.go('product_categories');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }
        function loadProductCategoryDetail() {
            apiService.get("/Api/ProductCategory/getbyid/" + $stateParams.id, null, function (result) {
                $scope.productCategory = result.data;
            }, function (error) {
                console.log(error);
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
        loadProductCategoryDetail();
    }

})(angular.module('tdshop.product_categories'))