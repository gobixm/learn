var app = angular.module('app', ["ui.router", "coordinator"])
    .config(function($stateProvider, $urlRouterProvider) {                  
        $stateProvider.state('coordinator', {
            url: "/coordinator",
            templateUrl: "src/coordinator.html",
            controller: "CoordinatorController"
        });
    })
    .controller('MainController', function($scope, $http) {
        $scope.appName = 'sample';
        $scope.children = [
            'coordinator1',
            'coordinator2',
            'coordinator3'
        ];
    });
