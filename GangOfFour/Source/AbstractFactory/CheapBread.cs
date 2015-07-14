using System;

namespace Infotecs.GangOfFour.AbstractFactory
{
    internal sealed class CheapBread : IProduct
    {
        public string Name
        {
            get { return GetType().Name; }
        }
    }
}
