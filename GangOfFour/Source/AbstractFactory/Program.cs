using System;

namespace Infotecs.GangOfFour.AbstractFactory
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ProductFactory productFactory = new CheapProductFactory();
            IProduct cheapBread = productFactory.CreateBread();
            IProduct cheapMilk = productFactory.CreateMilk();
            Console.WriteLine("I'm a {0}", cheapBread.Name);
            Console.WriteLine("I'm a {0}", cheapMilk.Name);

            productFactory = new LuxuryProductFactory();
            IProduct luxuryBread = productFactory.CreateBread();
            IProduct luxuryMilk = productFactory.CreateMilk();
            Console.WriteLine("I'm a {0}", luxuryBread.Name);
            Console.WriteLine("I'm a {0}", luxuryMilk.Name);
            Console.ReadKey();
        }
    }
}
