using System;

namespace Infotecs.GangOfFour.AbstractFactory
{
    internal sealed class LuxuryBread : IBread
    {
        public string Name
        {
            get { return GetType().Name; }
        }
    }
}
