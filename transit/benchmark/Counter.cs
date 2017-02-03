using System;
using System.Threading;

namespace transit.benchmark
{
    public static class Counter
    {
        private static int largeCount;
        private static int moderateCount;
        private static int smallCount;
        private static Timer timer;

        static Counter()
        {
            timer = new Timer(OnTime, null, 0, 1000);
        }

        public static void Stop()
        {
            timer.Dispose();
        }

        public static void CountLarge()
        {
            Interlocked.Increment(ref largeCount);
        }

        public static void CountModerate()
        {
            Interlocked.Increment(ref moderateCount);
        }

        public static void CountSmall()
        {
            Interlocked.Increment(ref smallCount);
        }

        private static void OnTime(object state)
        {
            Console.WriteLine($"recieved small:{smallCount} moderate:{moderateCount} large:{largeCount} /sec");
            smallCount = 0;
            moderateCount = 0;
            largeCount = 0;
        }
    }
}
