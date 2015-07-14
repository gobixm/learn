using System;

namespace Infotecs.GangOfFour.AbstractFactory
{
    internal sealed class LuxuryBread : IProduct
    {
        public string Name
        {
            get { return GetType().Name; }
        }
    }
}
