using System;

namespace Infotecs.GangOfFour.AbstractFactory
{
    internal sealed class CheapProductFactory : ProductFactory
    {
        public override IProduct CreateBread()
        {
            return new CheapBread();
        }

        public override IProduct CreateMilk()
        {
            return new CheapMilk();
        }
    }
}
