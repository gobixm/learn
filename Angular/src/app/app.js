var app = angular.module('app', ["ui.router", "coordinator", "role"])
    .config(function($stateProvider, $urlRouterProvider) {
        $stateProvider.state('coordinator', {
            url: "/coordinator/{id:int}",
            templateUrl: "src/coordinator.html",
            controller: "CoordinatorController"
        })
        .state('coordinator.role',{
            url: "/role/{role_id:int}",
            templateUrl: "src/role.html",
            controller: "RoleController"
        });
    })
    .controller('MainController', function($scope, $http) {
        $scope.appName = 'sample';
        $scope.coordinators = [{
            id: 1,
            name: 'coordinator1'
        }, {
            id: 2,
            name: 'coordinator2'
        }, {
            id: 3,
            name: 'coordinator3'
        }];
    });
