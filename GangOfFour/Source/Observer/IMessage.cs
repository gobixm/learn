using System;

namespace Infotecs.GangOfFour.Observer
{
    internal interface IMessage
    {
        string Body { get; set; }
        DateTime TimeStamp { get; set; }
    }
}
