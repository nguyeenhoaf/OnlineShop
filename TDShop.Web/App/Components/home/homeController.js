(function (app) {
    app.controller('homeController', homeController);

    function homeController() {
        console.log("Hello from category");
    }
})(angular.module('tdshop'));