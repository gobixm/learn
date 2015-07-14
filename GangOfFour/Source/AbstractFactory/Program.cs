using System;

namespace Infotecs.GangOfFour.AbstractFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            var cheapProductFactory = new CheapProductFactory();
            var cheapBread = cheapProductFactory.CreateBread();
            var cheapMilk = cheapProductFactory.CreateMilk();
            Console.WriteLine("I'm a {0}", cheapBread.Name);
            Console.WriteLine("I'm a {0}", cheapMilk.Name);
            
            var luxuryProductFactory = new LuxuryProductFactory();
            var luxuryBread = luxuryProductFactory.CreateBread();
            var luxuryMilk = luxuryProductFactory.CreateMilk();
            Console.WriteLine("I'm a {0}", luxuryBread.Name);
            Console.WriteLine("I'm a {0}", luxuryMilk.Name);
            Console.ReadKey();
        }
    }
}
