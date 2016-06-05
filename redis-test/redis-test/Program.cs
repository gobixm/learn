using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
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
            options.ResponseTimeout = 10000;
            options.SyncTimeout = 10000;
            //options.WriteBuffer = 400 * 1024 * 1024;
            var redis = ConnectionMultiplexer.Connect(options);
            var endpoint = redis.GetEndPoints(true).Single();
            var server = redis.GetServer(endpoint);
            server.FlushDatabase();
            server.FlushDatabase(1);
            server.FlushDatabase(2);
            var db = redis.GetDatabase();
            var dbRead = redis.GetDatabase(1);

            var sw = Stopwatch.StartNew();
            var generator = new MatrixGenerator();
            var side = 65535;
            generator.Generate(side, (index, data) => { db.StringSet($"matrix:{index}", data); });
            Console.WriteLine($"write. done in {sw.Elapsed} at {side/sw.Elapsed.TotalSeconds} writes/sec");

            //modify every bit in row
            sw.Restart();
            for (var i = 0; i < side; i++)
            {
                db.StringSetBit($"matrix:{0}", i, true);
            }
            Console.WriteLine(
                $"modify every single bit in row. done in {sw.Elapsed} at {side/sw.Elapsed.TotalSeconds} writes/sec");

            //modify bit in every row
            sw.Restart();
            for (var i = 0; i < side; i++)
            {
                db.StringSetBit($"matrix:{i}", i, true);
            }
            Console.WriteLine(
                $"modify single bit in every row. done in {sw.Elapsed} at {side/sw.Elapsed.TotalSeconds} writes/sec");

            //modify bit in every row in batch
            var tasks = new Task[side];
            sw.Restart();
            var batch = db.CreateBatch();
            for (var i = 0; i < side; i++)
            {
                tasks[i] = batch.StringSetBitAsync($"matrix:{i}", i, false);
            }
            batch.Execute();
            redis.WaitAll(tasks);
            Console.WriteLine(
                $"modify single bit in every row in batch. done in {sw.Elapsed} at {side/sw.Elapsed.TotalSeconds} writes/sec");

            //read all rows
            sw.Restart();
            var size = 0;
            for (var i = 0; i < side; i++)
            {
                size += ((byte[]) db.StringGet($"matrix:{i}")).Length;
            }
            Console.WriteLine(
                $"read all rows. done in {sw.Elapsed} at {side/sw.Elapsed.TotalSeconds} reads/sec total {size} bytes");

            //read all rows async
            sw.Restart();
            var reads = new Task[side];
            for (var i = 0; i < side; i++)
            {
                reads[i] = db.StringGetAsync($"matrix:{i}");
            }
            Task.WaitAll(reads);
            Console.WriteLine($"read all rows async. done in {sw.Elapsed} at {side/sw.Elapsed.TotalSeconds} reads/sec");

            //clone db
            sw.Restart();

            var clones = new List<Task>(side);
            foreach (var redisKey in server.Keys(0, "matrix:*"))
            {
                var task = db.KeyDumpAsync(redisKey).ContinueWith(t => dbRead.KeyRestoreAsync(redisKey, t.Result));
                clones.Add(task);
            }
            Task.WaitAll(clones.ToArray());
            Console.WriteLine($"clone all rows. done in {sw.Elapsed} at {side/sw.Elapsed.TotalSeconds} clone/sec");

            //clone db while modify
            var modifier = new MatrixModifier(db, side);
            modifier.StartModifying();
            Thread.Sleep(100);
            sw.Restart();
            clones = new List<Task>(side);
            foreach (var redisKey in server.Keys(0, "matrix:*"))
            {
                var task = db.KeyDumpAsync(redisKey).ContinueWith(t => dbRead.KeyRestoreAsync(redisKey, t.Result));
                clones.Add(task);
            }
            Task.WaitAll(clones.ToArray());
            var maxModify = modifier.StopModifying();
            Console.WriteLine(
                $"clone all rows while write. done in {sw.Elapsed} at {side/sw.Elapsed.TotalSeconds} clone/sec with max write delay {maxModify}");

            //clone db with lua
            var dbRead2 = redis.GetDatabase(2);
            modifier = new MatrixModifier(db, side);
            modifier.StartModifying();
            Thread.Sleep(100);
            var script = @"local keys = redis.call('keys', @mask)      
                              local dumps = {}                                   
                              for i = 1, #keys do dumps[i] = redis.call('dump', keys[i]) end                                                                                             
                              redis.call('select', @target)  
                              for i = 1, #dumps do redis.call('restore', keys[i], 0, dumps[i]) end";
            var prepared = LuaScript.Prepare(script);
            var loaded = prepared.Load(server);
            sw.Restart();
            loaded.Evaluate(db, new {mask = "matrix:*", target = dbRead2.Database});
            //db.ScriptEvaluate(script, new RedisKey[0], new RedisValue[] { "matrix:*", dbRead2.Database });
            maxModify = modifier.StopModifying();
            Console.WriteLine(
                $"clone all rows in lua while write. done in {sw.Elapsed} at {side/sw.Elapsed.TotalSeconds} clone/sec with max write delay {maxModify}");

            //delete row via lua
            sw.Restart();
            script = @"for i = 1, @side do
                            redis.call('setbit', 'matrix:' .. i, @index, 0)
                       end";
            prepared = LuaScript.Prepare(script);
            loaded = prepared.Load(server);
            sw.Restart();
            loaded.Evaluate(db, new {side, index = 0});
            batch.Execute();
            Console.WriteLine(
                $"modify single bit in every row via lua. done in {sw.Elapsed} at {side/sw.Elapsed.TotalSeconds} writes/sec");
            Console.ReadKey();
        }
    }
}