using System;

namespace Infotecs.GangOfFour.AbstractFactory
{
    internal sealed class LuxuryProductFactory : ProductFactory
    {
        public override IBread CreateBread()
        {
            return new LuxuryBread();
        }

        public override IMilk CreateMilk()
        {
            return new LuxuryMilk();
        }
    }
}
