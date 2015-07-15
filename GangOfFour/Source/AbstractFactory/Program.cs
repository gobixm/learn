using System;

namespace Infotecs.GangOfFour.AbstractFactory
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ProductFactory productFactory = new CheapProductFactory();
            IBread cheapBread = productFactory.CreateBread();
            IMilk cheapMilk = productFactory.CreateMilk();
            Console.WriteLine("I'm a {0}", cheapBread.Name);
            Console.WriteLine("I'm a {0}", cheapMilk.Name);

            productFactory = new LuxuryProductFactory();
            IBread luxuryBread = productFactory.CreateBread();
            IMilk luxuryMilk = productFactory.CreateMilk();
            Console.WriteLine("I'm a {0}", luxuryBread.Name);
            Console.WriteLine("I'm a {0}", luxuryMilk.Name);
            Console.ReadKey();
        }
    }
}
