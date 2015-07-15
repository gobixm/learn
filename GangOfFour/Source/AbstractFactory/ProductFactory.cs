using System;

namespace Infotecs.GangOfFour.AbstractFactory
{
    internal abstract class ProductFactory
    {
        public abstract IBread CreateBread();
        public abstract IMilk CreateMilk();
    }
}
