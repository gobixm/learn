using System;
using NUnit.Framework.Compatibility;

namespace HyberBench
{
    public class Bench
    {
        public static void Measure(string name, Action action)
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();
            action();
            sw.Stop();
            Console.WriteLine("{0} lasts {1} milliseconds", name, sw.ElapsedMilliseconds);
        }
    }
}