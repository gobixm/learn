using System;

namespace Infotecs.SOLID.InterfaceSegregation
{
    internal class Socket : IReader, IWriter
    {
        public void Read()
        {
            Console.WriteLine("read...");
        }

        public void Write()
        {
            Console.WriteLine("write...");
        }
    }
}