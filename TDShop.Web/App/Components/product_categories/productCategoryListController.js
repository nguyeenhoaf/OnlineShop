/// <reference path="../../../assets/admin/libs/angular/angular.js" />
/// <reference path="../../../assets/admin/libs/ngbootbox/ngbootbox.js" />
/// <reference path="../../../assets/admin/libs/bootbox/bootbox.js" />

(function (app) {
    app.controller('productCategoryListController', productCategoryListController);
    productCategoryListController.$inject = ['$scope', 'apiService', 'notificationService','$ngBootbox','$filter'];

    function productCategoryListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        //Property
        $scope.productCategories = [];
        $scope.firstFlag = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';
        $scope.isAll = false;

        //Event
        $scope.$watch("productCategories", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);
        //Method
        $scope.selectAll = function () {
            if ($scope.isAll == false) {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            }
            else {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }
        $scope.deleteMulti = function () {
            var listID = [];
            $.each($scope.selected, function (i, item) {
                listID.push(item.ID);
            });
            var config = {
                params: {
                    listID: JSON.stringify(listID)
                }
            }
            apiService.del('/Api/ProductCategory/deletemulti', config, function () {
                notificationService.displaySuccess('Xóa thành công');
                search();
            }, function () {
                notificationService.displayError('Xóa không thành công');
            });
        }
        $scope.getProductCagories = getProductCagories;
        $scope.search = search;
        function search () {
            getProductCagories();
        }
        $scope.DeleteProductCategory = DeleteProductCategory;
        function DeleteProductCategory(id) {
            $ngBootbox.confirm("Bạn có chắc chắn muốn xóa ?").then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
            apiService.del('/Api/ProductCategory/delete', config, function (result) {
                notificationService.displaySuccess('Xóa thành công');
                search();
            }, function (error) {
                notificationService.displayError('Xóa không thành công');
                });
            });
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