using System;

namespace Infotecs.GangOfFour.AbstractFactory
{
    internal sealed class CheapBread : IBread
    {
        public string Name
        {
            get { return GetType().Name; }
        }
    }
}
