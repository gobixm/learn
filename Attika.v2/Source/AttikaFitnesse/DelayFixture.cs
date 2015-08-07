using System;
using System.Threading;
using fitlibrary;

namespace Infotecs.Attika.AttikaFitnesse
{
    public class DelayFixture : DoFixture
    {
        public DelayFixture()
        {
        }

        public DelayFixture(int delay) : this()
        {
            Thread.Sleep(delay);
        }

        public void Wait(int delay)
        {
            Thread.Sleep(delay);
        }
    }
}
