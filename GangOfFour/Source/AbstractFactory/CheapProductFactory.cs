using System;

namespace Infotecs.GangOfFour.AbstractFactory
{
    internal sealed class CheapProductFactory : ProductFactory
    {
        public override IBread CreateBread()
        {
            return new CheapBread();
        }

        public override IMilk CreateMilk()
        {
            return new CheapMilk();
        }
    }
}
