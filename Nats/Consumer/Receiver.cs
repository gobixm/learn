using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using STAN.Client;

namespace Consumer
{
    public class Receiver : IDisposable
    {
        private readonly IStanConnection connection;
        private readonly Stopwatch stopwatch = new Stopwatch();
        private long messages = 0;
        private Timer timer;

        public Receiver(string id)
        {
            var stanConnectionFactory = new StanConnectionFactory();
            var options = StanOptions.GetDefaultOptions();
            connection = stanConnectionFactory.CreateConnection("test-cluster", id, options);
            timer = new Timer(Timer, null, 0, 1000);
        }

        private void Timer(object state)
        {
            Console.WriteLine($"{messages/((double)stopwatch.ElapsedMilliseconds/1000)} messages/sec");
            messages = 0;
            stopwatch.Restart();
        }

        public void Start()
        {
            var options = StanSubscriptionOptions.GetDefaultOptions();
            options.StartWithLastReceived();
            options.MaxInflight = 1000;
            connection.Subscribe("topic",
                "consumer-group",
                options,
                (sender, args) =>
                {
                    messages++;
                });
        }

        public void Dispose()
        {
            connection?.Dispose();
        }
    }
}
