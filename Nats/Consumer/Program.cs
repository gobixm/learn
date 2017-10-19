using System;
using System.Threading.Tasks;

namespace Consumer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var receiver = new Receiver(Guid.NewGuid().ToString()))
            {
                receiver.Start();
                Task.Delay(1000000000).Wait();
            }
        }
    }
}
