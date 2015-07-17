using System;

namespace Infotecs.GangOfFour.Observer
{
    internal sealed class Message : IMessage
    {
        public Message()
        {
            Body = "earthquake";
            TimeStamp = DateTime.Now;
        }

        public string Body { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
