using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using KafkaNet;
using KafkaNet.Model;
using KafkaNet.Protocol;

namespace kafka
{
    public class Producer
    {
        private KafkaNet.Producer _producer;

        public void Run()
        {
            var options = new KafkaOptions(new Uri("http://localhost:9092"));
            var router = new BrokerRouter(options);
            _producer = new KafkaNet.Producer(router);
            Process();
        }

        private async void Process()
        {
            var counter = 1000;
            while (true)
            {
                Thread.Sleep(100);
                var msg = new Message($"message {counter++}");
                msg.Key = BitConverter.GetBytes((ulong)(DateTime.UtcNow - DateTime.MinValue).TotalMilliseconds);
                var resp = await _producer.SendMessageAsync("test", new List<Message> {msg});
                resp.ForEach(x =>
                {
                    Console.WriteLine($"produce - {Encoding.UTF8.GetString(msg.Value)}, partition - {x.PartitionId}, offset - {x.Offset}");
                });
                
            }
        }
    }
}