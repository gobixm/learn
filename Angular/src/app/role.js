angular.module('role', [])
    .controller('RoleController', function($scope, $http, $stateParams) {
        if ($stateParams.role_id === undefined) {
            return;
        }

        $scope.role = $scope.coordinator.roles.filter(function(x) {
            return x.id === $stateParams.role_id;
        })[0];        
    });
