using System;

namespace Infotecs.GangOfFour.AbstractFactory
{
    internal sealed class CheapMilk : IMilk
    {
        public string Name
        {
            get { return GetType().Name; }
        }
    }
}
