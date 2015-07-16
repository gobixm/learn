using System;

namespace Infotecs.GangOfFour.Decorator
{
    internal class Report : IReport
    {
        public void Print()
        {
            Console.WriteLine("Simple report");
        }
    }
}
