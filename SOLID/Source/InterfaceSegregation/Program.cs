using System;

namespace Infotecs.SOLID.InterfaceSegregation
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var socket = new Socket();
            var reader = (IReader) socket;
            var writer = (IWriter) socket;
            reader.Read();
            writer.Write();
            Console.ReadKey();
        }
    }
}