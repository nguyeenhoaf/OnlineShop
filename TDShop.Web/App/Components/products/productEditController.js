(function (app) {
    app.controller('productEditController', productEditController);
    productEditController.$inject = ['apiService', '$scope', '$stateParams', 'notificationService', '$state', 'commonService'];
    function productEditController(apiService, $scope, $stateParams, notificationService, $state, commonService) {
        $scope.product = { CreatedDate: new Date()}
        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px'
        }
        $scope.moreImages = [];
        $scope.UpdateProduct = UpdateProduct;
        $scope.$watch('product.Name', function () {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        });
        function UpdateProduct() {
            $scope.product.MoreImages = JSON.stringify($scope.moreImages);
            apiService.put('/Api/Product/update', $scope.product,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật.');
                    $state.go('products');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }
        function loadProductCategory() {
            apiService.get('/Api/ProductCategory/getallparents', null, function (result) {
                $scope.productCategories = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }
        function loadProductDetail() {
            apiService.get("/Api/Product/getbyid/" + $stateParams.id, null, function (result) {
                $scope.product = result.data;
                $scope.moreImages = JSON.parse($scope.product.MoreImages);
            }, function (error) {
                console.log(error);
            });
        }
        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;
                });
               
            }
            finder.popup();
        }
        loadProductCategory();
        loadProductDetail();
        $scope.ChooseMoreImage = function () {
            if ($scope.moreImages == null) {
                $scope.moreImages = [];
            }
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                });
               
            }
            finder.popup()
        }

    }
})(angular.module('tdshop.products'));