using System;
using System.Linq;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Impl;

namespace emitter
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "topic_logs",
                    type: "topic",
                    durable: true);

                var routingKey = args.Length > 0 ? args[0] : "topic.sharding";

                for (int i = 0; i < 10000; i++) {
                    var message = $"Hello World - {i}! ({routingKey})";
                    var body = Encoding.UTF8.GetBytes(message);
                    BasicProperties props = new RabbitMQ.Client.Framing.BasicProperties();
                    props.Persistent = true;
                    channel.BasicPublish(exchange: "topic_logs",
                        routingKey: routingKey,
                        basicProperties: props,
                        body: body);
                }
                Console.WriteLine("done");
            }
        }
    }
}