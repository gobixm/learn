using System;

namespace Infotecs.GangOfFour.Proxy
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            RealResource theResource = new RealResource();
            ControlledResource cr = new ControlledResource(theResource, "Petrov");
            cr.Erase();
            cr = new ControlledResource(theResource, "Ivanov");
            cr.Erase();
            Console.ReadKey();
        }
    }
}
