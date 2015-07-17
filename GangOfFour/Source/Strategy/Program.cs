using System;

namespace Infotecs.GangOfFour.Strategy
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var chiper = new Chiper(new WeakGenerator());
            chiper.Encode("text");
            chiper.Generator = new StrongGenerator();
            chiper.Encode("text");
            Console.ReadKey();
        }
    }
}
