using System;

namespace Infotecs.GangOfFour.Strategy
{
    class Program
    {
        static void Main(string[] args)
        {
            var chiper = new Chiper(new WeakGenerator());
            chiper.Encode("text");
            chiper.Generator = new StrongGenerator();
            chiper.Encode("text");
            Console.ReadKey();
        }
    }
}
