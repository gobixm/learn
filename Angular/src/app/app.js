var app = angular.module('app', ["ui.router", "pascalprecht.translate", "angularMoment", "coordinator", "role"])
    .config(function($stateProvider, $urlRouterProvider, $translateProvider) {
        $stateProvider.state('coordinator', {
                url: "/coordinator/{id:int}",
                templateUrl: "src/coordinator.html",
                controller: "CoordinatorController"
            })
            .state('coordinator.role', {
                url: "/role/{role_id:int}",
                templateUrl: "src/role.html",
                controller: "RoleController"
            });

        $translateProvider.translations('en', {
                APPNAME: 'Sample',
                RU_BUTTON_TEXT: 'Русский',
                EN_BUTTON_TEXT: 'English',
                COORDINATOR: 'Coordinator',
                ROLE: 'Role',
                ROLES: 'Roles',
                CURRENT_DATE: 'Now is {{date | date: "short"}}'
            })
            .translations('ru', {
                APPNAME: 'Пример',
                RU_BUTTON_TEXT: 'Русский',
                EN_BUTTON_TEXT: 'English',
                COORDINATOR: 'Координатор',
                ROLE: 'Роль',
                ROLES: 'Роли',
                CURRENT_DATE: 'Сейчас {{date | date: "short"}}'
            });
        $translateProvider.preferredLanguage('ru');
    })
    .controller('MainController', function($scope, $http, $translate) {
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

        $scope.changeLanguage = function(lang) {
            $translate.use(lang);
            moment.locale(lang);
        };

        $scope.currentTime = function() {
            return moment().format('LLL');
        }
    });
