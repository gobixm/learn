using System;

namespace Infotecs.GangOfFour.ChainOfResponsibility
{
    internal sealed class FirstRequest : IRequest
    {
        public string Body
        {
            get { return GetType().Name; }
        }
    }
}
