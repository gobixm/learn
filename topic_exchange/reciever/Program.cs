using System;
using System.Collections.Concurrent;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace reciever
{
    internal class Program
    {
        private static readonly object modelLock = new object();

        private static void Main(string[] args) {
            var knownRoutingKeys = new ConcurrentDictionary<string, IModel>();
            var factory = new ConnectionFactory {HostName = "localhost"};
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel()) {
                channel.ExchangeDeclare("topic_logs", "topic", true);
                channel.QueueDeclare("sharding_all", true, autoDelete: false, exclusive: false);
                channel.BasicQos(0, 0, false);

                channel.QueueBind("sharding_all",
                    "topic_logs",
                    "topic.sharding.*");

                Console.WriteLine(" [*] Waiting for messages. To exit press CTRL+C");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received +=
                    (model, ea) => {
                        var localRoutingKey = $"local.{ea.RoutingKey}";
                        if (!knownRoutingKeys.ContainsKey(localRoutingKey))
                            lock (modelLock) {
                                var localChannel = connection.CreateModel();
                                QueueDeclareOk result = localChannel.QueueDeclare(localRoutingKey, true, false, false);

                                localChannel.BasicQos(0, 1, false);
                                localChannel.QueueBind(localRoutingKey,
                                    "topic_logs",
                                    localRoutingKey);

                                var localConsumer = new EventingBasicConsumer(localChannel);
                                knownRoutingKeys.TryAdd(localRoutingKey, localChannel);
                                localConsumer.Received += ConsumerOnReceived;
                                localChannel.BasicConsume(queue: localRoutingKey,
                                    consumer: localConsumer);
                                if (result.MessageCount == 0) Console.WriteLine("creating new");
                            }


                        knownRoutingKeys[localRoutingKey]
                            .BasicPublish("topic_logs",
                                localRoutingKey,
                                null,
                                ea.Body);

                        lock (modelLock) {
                            ((IBasicConsumer) model).Model.BasicAck(ea.DeliveryTag, false);
                        }
                    };
                channel.BasicConsume(queue: "sharding_all",
                    consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }

        private static void ConsumerOnReceived(object model, BasicDeliverEventArgs ea) {
            Task.Run(() => {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                var routingKey = ea.RoutingKey;
                Console.WriteLine(" [x] Received '{0}':'{1}'",
                    routingKey,
                    message);

                lock (modelLock) {
                    ((IBasicConsumer) model).Model.BasicAck(ea.DeliveryTag, false);
                }
            });
        }
    }
}