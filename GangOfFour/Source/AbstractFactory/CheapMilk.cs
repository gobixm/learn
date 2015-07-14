using System;

namespace Infotecs.GangOfFour.AbstractFactory
{
    internal sealed class CheapMilk : IProduct
    {
        public string Name
        {
            get { return GetType().Name; }
        }
    }
}
