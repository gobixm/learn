using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalApp.Models
{
    public class GameEvent
    {
        public string EventName { get; private set; }
        public GameEvent(string eventName)
        {
            EventName = eventName;
        }
    }
}