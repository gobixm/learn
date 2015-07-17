using System;

namespace Infotecs.SOLID.Liskov
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Tunnel tunnel = new SpecificTunnel();
            try
            {
                tunnel.Open("non existing resource");
            }
            catch (TunnelNotExistsException e)
            {
                Console.WriteLine("Exception of type {0} caught", e.GetType());
            }
            Console.ReadKey();
        }
    }
}