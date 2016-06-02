using System;
using System.Diagnostics;
using StackExchange.Redis;

namespace redis_test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var options = ConfigurationOptions.Parse("localhost");
            options.SyncTimeout = 10000;
            //options.WriteBuffer = 400 * 1024 * 1024;
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(options);
            IDatabase db = redis.GetDatabase();
            byte[] data = new byte[300 * 1024 * 1024];
            for (int i = 0; i < 10* 1024; i++)
            {
                Stopwatch sw = Stopwatch.StartNew();
                db.HashSet("org", i, data);
                Console.WriteLine($"publish - {sw.Elapsed}");
                sw.Restart();
                var incoming = (byte[])db.HashGet("org", i);
                Console.WriteLine($"consume {incoming.Length} bytes - {sw.Elapsed}");
                sw.Restart();
                db.HashDelete("org", i);
                Console.WriteLine($"remove - {sw.Elapsed}");
            }
            Console.WriteLine("done");
            Console.ReadKey();
        }
    }
}