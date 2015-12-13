using SignalApp.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SignalApp
{
    public class GameService : IGameService
    {
        private const double FieldWidth = 800;
        private const double FieldHeight = 600;
        private Random _random = new Random();
        private ConcurrentDictionary<string, Player> _players = new ConcurrentDictionary<string, Player>();
        private ConcurrentQueue<GameEvent> _eventQueue = new ConcurrentQueue<GameEvent>();
        private List<Action<GameEvent>> _notify = new List<Action<GameEvent>>();
        private CancellationTokenSource _cancelNotification = new CancellationTokenSource();

        public ConcurrentDictionary<string, Player> Players
        {
            get
            {
                return _players;
            }
        }

        public GameService()
        {
            Task.Factory.StartNew(() =>
            {
                while (!_cancelNotification.IsCancellationRequested)
                {
                    ProcessWorld();
                    while (_eventQueue.Any())
                    {
                        GameEvent gameEvent;
                        _eventQueue.TryDequeue(out gameEvent);
                        var notificationList = _notify.ToList();
                        notificationList.ForEach(x => x(gameEvent));
                    }

                    Task.Delay(100);
                }
            },
            _cancelNotification.Token);
        }

        public Player SpawnPlayer(string playerUid)
        {
            var player = new Player(playerUid, _random.NextDouble() * FieldWidth, _random.NextDouble() * FieldHeight);
            _players[playerUid] = player;
            _eventQueue.Enqueue(new NewPlayerEvent(player));
            return player;
        }

        public void ProcessWorld()
        {
            LimitBounds().ForEach(x => _eventQueue.Enqueue(x));
        }

        private List<GameEvent> LimitBounds()
        {
            var result = new List<GameEvent>();
            foreach (var player in _players.Values)
            {
                if (player.X > FieldWidth || player.Y > FieldHeight || player.X < 0 || player.Y < 0)
                {
                    player.Bound(FieldWidth, FieldHeight);
                    result.Add(new MoveEvent(player));
                }
            }
            return result;
        }

        public void DestroyPlayer(string playerUid)
        {
            Player player;
            _players.TryRemove(playerUid, out player);
            _eventQueue.Enqueue(new DestroyPlayerEvent(player));
        }

        public void Subscribe(Action<GameEvent> onEvent)
        {
            _notify.Add(onEvent);
        }

        public void Move(string playerUid, double x, double y)
        {
            var player = Players[playerUid];
            player.X = x;
            player.Y = y;
            _eventQueue.Enqueue(new MoveEvent(player));
        }

        public void Unsubscribe(Action<GameEvent> onEvent)
        {
            _notify.Remove(onEvent);
        }
    }
}