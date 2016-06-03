using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace redis_test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var options = ConfigurationOptions.Parse("localhost");
            options.AllowAdmin = true;
            options.ResponseTimeout = 1000;
            options.SyncTimeout = 1000;
            //options.WriteBuffer = 400 * 1024 * 1024;
            var redis = ConnectionMultiplexer.Connect(options);
            var endpoint = redis.GetEndPoints(true).Single();
            var server = redis.GetServer(endpoint);
            server.FlushDatabase();
            server.FlushDatabase(1);
            var db = redis.GetDatabase();
            var dbRead = redis.GetDatabase(1);



            var sw = Stopwatch.StartNew();
            var generator = new MatrixGenerator();
            var side = 100000;
            generator.Generate(side, (index, data) => { db.StringSet($"matrix:{index}", data); });
            Console.WriteLine($"write. done in {sw.Elapsed} at {side / sw.Elapsed.TotalSeconds} writes/sec");

            //modify every bit in row
            sw.Restart();
            for (int i = 0; i < side; i++)
            {
                db.StringSetBit($"matrix:{0}", i, true);
            }
            Console.WriteLine($"modify every single bit in row. done in {sw.Elapsed} at {side / sw.Elapsed.TotalSeconds} writes/sec");

            //modify bit in every row
            sw.Restart();
            for (int i = 0; i < side; i++)
            {
                db.StringSetBit($"matrix:{i}", i, true);
            }
            Console.WriteLine($"modify single bit in every row. done in {sw.Elapsed} at {side / sw.Elapsed.TotalSeconds} writes/sec");

            //modify bit in every row in batch
            var tasks = new Task[side];
            sw.Restart();
            var batch = db.CreateBatch();
            for (int i = 0; i < side; i++)
            {
                tasks[i] = batch.StringSetBitAsync($"matrix:{i}", i, false);
            }
            batch.Execute();
            redis.WaitAll(tasks);
            Console.WriteLine($"modify single bit in every row in batch. done in {sw.Elapsed} at {side / sw.Elapsed.TotalSeconds} writes/sec");

            //read all rows
            sw.Restart();
            int size = 0;
            for (int i = 0; i < side; i++)
            {
                size += ((byte[])db.StringGet($"matrix:{i}")).Length;
            }
            Console.WriteLine($"read all rows. done in {sw.Elapsed} at {side / sw.Elapsed.TotalSeconds} reads/sec total {size} bytes");

            //read all rows async
            sw.Restart();
            var reads = new Task[side];
            for (int i = 0; i < side; i++)
            {
                reads[i] = db.StringGetAsync($"matrix:{i}");
            }
            Task.WaitAll(reads);
            Console.WriteLine($"read all rows async. done in {sw.Elapsed} at {side / sw.Elapsed.TotalSeconds} reads/sec");

            //clone db
            sw.Restart();

            var clones = new List<Task>(side);
            foreach (var redisKey in server.Keys(0, "matrix:*"))
            {
                var task = db.KeyDumpAsync(redisKey).ContinueWith(t => dbRead.KeyRestoreAsync(redisKey, t.Result));
                clones.Add(task);
            }
            Task.WaitAll(clones.ToArray());
            Console.WriteLine($"clone all rows. done in {sw.Elapsed} at {side / sw.Elapsed.TotalSeconds} clone/sec");

            Console.ReadKey();
        }
    }
}