/// <reference path="../plugin/angular/angular.js" />

var myApp = angular.module('myModule', []);
myApp.controller("schoolController", schoolController);
myApp.directive("tdShopDirective", tDShopDirective);
myApp.service("ValidatorService", ValidatorService);

schoolController.$inject = ['$scope', 'ValidatorService'];
function schoolController($scope, ValidatorService) {
    
    $scope.checkNumber = function () {
        $scope.message = ValidatorService.checknumber($scope.num);
    }
    $scope.num = 1;
}

function ValidatorService($window) {
    return {
        checknumber: checkNumber
    }
    function checkNumber(input) {
        if (input % 2 == 0)
            return 'This is even';
        else
            return 'This is odd';
    }
}

function tDShopDirective() {
    return {
        template:"<h1>This is Custom Directive</h1>"
    }
}