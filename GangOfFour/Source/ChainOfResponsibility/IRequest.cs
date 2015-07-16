using System;

namespace Infotecs.GangOfFour.ChainOfResponsibility
{
    internal interface IRequest
    {
        string Body { get; }
    }
}
