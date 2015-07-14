using System;

namespace Infotecs.GangOfFour.AbstractFactory
{
    internal sealed class LuxuryMilk : IProduct
    {
        public string Name
        {
            get { return GetType().Name; }
        }
    }
}
