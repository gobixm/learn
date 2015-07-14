using System;

namespace Infotecs.GangOfFour.AbstractFactory
{
    internal abstract class ProductFactory
    {
        public abstract IProduct CreateBread();
        public abstract IProduct CreateMilk();
    }
}
