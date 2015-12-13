using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalApp.Models
{
    public class MoveEvent : GameEvent
    {
        public Player Player { get; private set; }
        public MoveEvent(Player playerWithNewPosition):base("move")
        {
            Player = playerWithNewPosition;
        }
    }
}