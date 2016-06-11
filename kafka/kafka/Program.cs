using System;
using System.Linq;

namespace kafka
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var producer = new Producer();
            producer.Run();

            var consumers = Enumerable
                .Range(0, 5)
                .Select(x => new Consumer())
                .ToList();

            consumers.ForEach(x => x.Run());

            Console.ReadKey();
        }
    }
}