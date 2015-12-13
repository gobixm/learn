var app = angular.module('app', [])
    .controller('AppController', function ($scope, $http) {
        $scope.canvas = new fabric.Canvas('main_canvas');

        $scope.player = { data: null, sprite: null };
        hub = $.connection.gameHub;
        $scope.players = [];

        hub.client.notify = function (data) {
            if (data.EventName === "move") {
                var player = $scope.findPlayer(data.Player.Name);                
                if (player !== undefined) {
                    if (Math.floor(player.data.X) === Math.floor(data.Player.X) && Math.floor(player.data.Y) === Math.floor(data.Player.Y)) {
                        return;
                    }
                    player.data = data.Player;
                    player.sprite.left = player.data.X;
                    player.sprite.top = player.data.Y;                   
                    $scope.canvas.renderAll();
                }                
            }
            if (data.EventName === "new_player") {                
                var player = $scope.findPlayer(data.Player.Name);
                if (player !== undefined) {
                    return;
                }
                if (data.Player.Name !== $scope.player.data.Name) {
                    console.debug(data);
                    var player = { data: null, sprite: null };
                    var circle = new fabric.Circle({
                        radius: 20, fill: 'red', left: data.Player.X, top: data.Player.Y, selectable: false
                    });
                    player.sprite = circle;
                    player.data = data.Player;
                    $scope.canvas.add(circle);
                    $scope.players.push(player);
                }
            }
            if (data.EventName === "destroy_player") {
                $scope.destroyPlayer(data.Player.Name);
                $scope.canvas.renderAll();
            }
        }

        $scope.findPlayer = function (playerUid) {            
            var i = 0;
            for (i = 0; i < $scope.players.length; i++) {
                if ($scope.players[i].data.Name === playerUid) {                    
                    return $scope.players[i];
                }
            }
        }

        $scope.destroyPlayer = function (playerUid) {
            var i = 0;
            for (i = 0; i < $scope.players.length;) {
                if ($scope.players[i].data.Name === playerUid) {
                    $scope.canvas.remove($scope.players[i].sprite);
                    $scope.players.splice(i, 1);
                }
                else {
                    i++;
                }
            }
        }

        $.connection.hub.start().done(function () {
            hub.server.connect()
                .done(function (player) {
                    console.debug('connect');
                    $scope.player.data = player;
                    var circle = new fabric.Circle({
                        radius: 20, fill: 'green', left: $scope.player.data.X, top: $scope.player.data.Y, selectable: true
                    });
                    circle.on('moving', function (event) {                        
                        if (Math.floor($scope.player.data.X) === Math.floor(circle.left) && Math.floor($scope.player.data.Y) === Math.floor(circle.top)) {
                            return;                            
                        }

                        $scope.player.data.X = circle.left;
                        $scope.player.data.Y = circle.top;                        
                        hub.server.move($scope.player.data.X, $scope.player.data.Y);
                    });
                    $scope.player.sprite = circle;
                    $scope.canvas.add(circle);
                    $scope.players.push($scope.player);

                    hub.server.getPlayers();
                });
        });

        $scope.$on('$destroy', function () {
            hub.server.disconnect();
        });
    });