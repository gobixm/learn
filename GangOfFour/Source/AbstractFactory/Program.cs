using System;

namespace Infotecs.GangOfFour.AbstractFactory
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var cheapProductFactory = new CheapProductFactory();
            IProduct cheapBread = cheapProductFactory.CreateBread();
            IProduct cheapMilk = cheapProductFactory.CreateMilk();
            Console.WriteLine("I'm a {0}", cheapBread.Name);
            Console.WriteLine("I'm a {0}", cheapMilk.Name);

            var luxuryProductFactory = new LuxuryProductFactory();
            IProduct luxuryBread = luxuryProductFactory.CreateBread();
            IProduct luxuryMilk = luxuryProductFactory.CreateMilk();
            Console.WriteLine("I'm a {0}", luxuryBread.Name);
            Console.WriteLine("I'm a {0}", luxuryMilk.Name);
            Console.ReadKey();
        }
    }
}
