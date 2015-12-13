using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalApp.Models
{
    public class NewPlayerEvent : GameEvent
    {
        public Player Player { get; private set; }
        public NewPlayerEvent(Player player) : base("new_player")
        {
            Player = player;
        }
    }
}