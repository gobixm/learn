using System;

namespace Infotecs.GangOfFour.ChainOfResponsibility
{
    internal sealed class SecondRequest : IRequest
    {
        public string Body
        {
            get { return GetType().Name; }
        }
    }
}
