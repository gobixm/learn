using System;

namespace Infotecs.GangOfFour.ChainOfResponsibility
{
    internal class FirstRequest : IRequest
    {
        public string Body
        {
            get { return GetType().Name; }
        }
    }
}
