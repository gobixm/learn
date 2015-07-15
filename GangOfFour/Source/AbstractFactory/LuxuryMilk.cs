using System;

namespace Infotecs.GangOfFour.AbstractFactory
{
    internal sealed class LuxuryMilk : IMilk
    {
        public string Name
        {
            get { return GetType().Name; }
        }
    }
}
