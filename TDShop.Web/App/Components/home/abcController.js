(function (app) {
    app.controller('abcController', abcController);

    function abcController() {
        console.log("Hello from category");
    }
})(angular.module('tdshop'));