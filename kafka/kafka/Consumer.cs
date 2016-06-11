using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KafkaNet;
using KafkaNet.Model;
using KafkaNet.Protocol;

namespace kafka
{
    public class Consumer
    {
        private static int _instance;
        private readonly int _current;

        private KafkaNet.Consumer _consumer;

        public Consumer()
        {
            _current = Interlocked.Increment(ref _instance);
        }

        public void Run()
        {
            var options = new KafkaOptions(new Uri("http://localhost:9092"));
            var router = new BrokerRouter(options);
            var consumerOptions = new ConsumerOptions("test", router);           
            _consumer = new KafkaNet.Consumer(consumerOptions, new OffsetPosition(0, 100));
            Process();
        }

        private async void Process()
        {
            var startTime = DateTime.Now.ToUniversalTime();
            await Task.Run(() =>
            {
                foreach (var message in _consumer.Consume())
                {
                    var ticks = BitConverter.ToUInt64(message.Key, 0);
                    var time = DateTime.MinValue + TimeSpan.FromMilliseconds(ticks);
                    
                    if (time > startTime)
                    {
                        Console.WriteLine($"consumer {_current} - {Encoding.UTF8.GetString(message.Value)}");
                    }
                }
            });
        }
    }
}