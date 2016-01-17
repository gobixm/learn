var app = angular.module('app', [])
    .controller('AppController', function ($scope, $http) {
        $scope.canvas = new fabric.Canvas('main_canvas');

        $scope.player = { data: null, sprite: null };
        hub = $.connection.gameHub;
        $scope.players = [];
        $scope.lastEvent = '';
        $scope.eventsPerSecond = 0;
        $scope.eventCounter = 0;

        hub.client.notify = function (data) {
            $scope.eventCounter++;
            $scope.$apply(function () {
                $scope.lastEvent = JSON.stringify(data, null, 4);
            });
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
                        radius: 20,
                        fill: data.Player.Color,
                        left: data.Player.X,
                        top: data.Player.Y,
                        selectable: false,
                        opacity: 0
                    });
                    player.sprite = circle;
                    player.data = data.Player;
                    $scope.canvas.add(circle);
                    $scope.players.push(player);
                    player.sprite.animate('opacity', 1, {
                        duration: 1000,
                        onChange: $scope.canvas.renderAll.bind($scope.canvas)
                    });
                }
            }
            if (data.EventName === "destroy_player") {
                $scope.destroyPlayer(data.Player.Name);
                $scope.canvas.renderAll();
            }
        }

        $scope.countEvents = function () {
            $scope.eventCounter = 0;
            setTimeout(function () {
                $scope.$apply(function () {
                    $scope.eventsPerSecond = $scope.eventCounter;
                });                
                $scope.countEvents();
            },
            1000);
        }

        $scope.findPlayer = function (playerUid) {
            return _.find($scope.players, function (player) {
                return player.data.Name === playerUid;
            });
        }

        $scope.destroyPlayer = function (playerUid) {
            $scope.players = _.reject($scope.players, function (player) {
                if (player.data.Name === playerUid) {
                    var sprite = player.sprite;
                    player.sprite.animate('opacity', 0, {
                        duration: 1000,
                        onChange: $scope.canvas.renderAll.bind($scope.canvas),
                        onComplete: function () {
                            $scope.canvas.remove(sprite);
                        }
                    });
                    return true;
                }
            });
        }

        $scope.countEvents();

        $.connection.hub.start().done(function () {
            hub.server.connect()
                .done(function (player) {
                    console.debug('connect');
                    $scope.player.data = player;
                    var circle = new fabric.Circle({
                        radius: 20, fill: $scope.player.data.Color, left: $scope.player.data.X, top: $scope.player.data.Y, selectable: true
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