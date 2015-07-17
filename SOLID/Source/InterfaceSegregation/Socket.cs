using System;

namespace Infotecs.SOLID.InterfaceSegregation
{
    internal sealed class Socket : IReader, IWriter
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