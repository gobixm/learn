using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace redis_test
{
    public class MatrixModifier
    {
        private readonly IDatabase _database;
        private readonly int _side;
        private CancellationTokenSource _cts;
        private TimeSpan _max = TimeSpan.Zero;

        public MatrixModifier(IDatabase database, int side)
        {
            _database = database;
            _side = side;
        }

        public void StartModifying()
        {
            _cts = new CancellationTokenSource();
            Task.Run(() =>
            {
                Random rnd = new Random();
                while (true)
                {
                    if (_cts.IsCancellationRequested)
                    {
                        return;
                    }
                    var x = rnd.Next(_side);
                    var y = rnd.Next(_side);
                    Stopwatch sw = Stopwatch.StartNew();
                    _database.StringSetBit($"matrix:{x}", y, x % 2 == 0);
                    _database.StringSetBit($"matrix:{y}", x, x % 2 == 0);
                    _max = sw.Elapsed > _max ? sw.Elapsed : _max;
                }
            });
        }

        public TimeSpan StopModifying()
        {
            _cts?.Cancel();
            return _max;
        }
    }
}