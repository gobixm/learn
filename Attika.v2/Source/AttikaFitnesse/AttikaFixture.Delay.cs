using System;

namespace Infotecs.Attika.AttikaFitnesse
{
    partial class AttikaFixture
    {
        public DelayFixture DelayFixture(int delay)
        {
            return new DelayFixture(delay);
        }
    }
}
