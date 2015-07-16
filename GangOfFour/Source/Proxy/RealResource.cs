using System;

namespace Infotecs.GangOfFour.Proxy
{
    internal class RealResource : IResource
    {
        public void Erase()
        {
            Console.WriteLine("All information erased");
        }
    }
}
