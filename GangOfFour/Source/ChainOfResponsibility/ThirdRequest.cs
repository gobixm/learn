using System;

namespace Infotecs.GangOfFour.ChainOfResponsibility
{
    internal class ThirdRequest : IRequest
    {
        public string Body
        {
            get { return GetType().Name; }
        }
    }
}
