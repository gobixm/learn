using System;

namespace Infotecs.GangOfFour.Singleton
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine(Singleton.Instance.Field);
            Console.ReadKey();
        }
    }
}
