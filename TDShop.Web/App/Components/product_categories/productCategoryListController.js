(function (app) {
    app.controller('productCategoryListController', productCategoryListController);
    productCategoryListController.$inject = ['$scope', 'apiService', 'notificationService'];

    function productCategoryListController($scope, apiService, notificationService) {
        $scope.productCategories = [];
        $scope.firstFlag = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';
        $scope.getProductCagories = getProductCagories;
        $scope.search = function () {
            getProductCagories();
        }
        $scope.DeleteProductCategory = DeleteProductCategory;
        function DeleteProductCategory(id) {
            var config = {
                params: {
                    id: id
                }
            }
            apiService.del("/Api/ProductCategory/delete/", config, function (result) {
                notificationService.displaySuccess("Xóa thành công " + result.data.Name);
                $scope.getProductCagories();
            }, function (error) {
                notificationService.displayError("Xóa thất bại !!");
            }
            )
        }
        function getProductCagories(page, keyword) {
            var page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize : 2
                }
            }
            apiService.get('/API/ProductCategory/getall', config, function (result) {
                if (result.data.TotalCount == 0 && $scope.page == 0) {
                    notificationService.displayWarning("Không tìm thấy bản ghi nào !!");
                }
                else {
                    if ($scope.page == 0 && $scope.firstFlag) {
                        notificationService.displaySuccess("Tìm được " + result.data.TotalCount + " bản ghi");
                        $scope.firstFlag = false;
                    }
                       
                }
                $scope.productCategories = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load productcategory failed.');
            });
        }
        $scope.getProductCagories();
    }
})(angular.module('tdshop.product_categories'));