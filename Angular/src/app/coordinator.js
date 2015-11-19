angular.module('coordinator', ["pascalprecht.translate", 'role'])
    .controller('CoordinatorController', function($scope, $http, $stateParams) {
        if ($stateParams.id === undefined) {
            return;
        }

        $scope.coordinator = $scope.coordinators.filter(function(x) {        	
            return x.id === $stateParams.id;
        })[0];
        $scope.coordinator.roles =[
        	{id:$scope.coordinator.id*100+1, name:$scope.coordinator.name+"'s first role"},
        	{id:$scope.coordinator.id*100+2, name:$scope.coordinator.name+"'s second role"},
        	{id:$scope.coordinator.id*100+3, name:$scope.coordinator.name+"'s third role"},
        ];
    });
