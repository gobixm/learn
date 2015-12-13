using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalApp.Models
{
    public class DestroyPlayerEvent : GameEvent
    {
        public Player Player { get; private set; }
        public DestroyPlayerEvent(Player player) : base("destroy_player")
        {
            Player = player;
        }
    }
}