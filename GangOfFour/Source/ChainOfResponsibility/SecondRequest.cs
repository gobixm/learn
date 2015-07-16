using System;

namespace Infotecs.GangOfFour.ChainOfResponsibility
{
    internal class SecondRequest : IRequest
    {
        public string Body
        {
            get { return GetType().Name; }
        }
    }
}
