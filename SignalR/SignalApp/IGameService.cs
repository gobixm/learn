using SignalApp.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalApp
{
    public interface IGameService
    {
        Player SpawnPlayer(string playerUid);
        void DestroyPlayer(string playerUid);
        void ProcessWorld();
        ConcurrentDictionary<string, Player> Players { get; }
        void Subscribe(Action<GameEvent> onEvent);
        void Unsubscribe(Action<GameEvent> onEvent);
        void Move(string playerUid, double x, double y);
    }
}
