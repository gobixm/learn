using System;

namespace Infotecs.GangOfFour.ChainOfResponsibility
{
    internal sealed class ThirdRequest : IRequest
    {
        public string Body
        {
            get { return GetType().Name; }
        }
    }
}
