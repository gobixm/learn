using System;
using System.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infotecs.Attika.AttikaDomain.Services.Queuing
{
    public class QueueService : IQueueService
    {
        private readonly IModel _channel;
        private readonly IConnection _connection;
        private readonly ConnectionFactory _factory;
        private Action<byte[]> _arrival;

        public QueueService()
        {
            var host = ConfigurationManager.ConnectionStrings["rabbit"];
            var hostname = "localhost";
            if (host != null)
                hostname = host.ConnectionString;

            _factory = new ConnectionFactory
            {
                HostName = hostname
            };

            _arrival = (bytes => { });

            _connection = _factory.CreateConnection();

            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare("attika_exchange", ExchangeType.Fanout);
            _channel.QueueDeclare("attika_queue", true, false, false, null);
            _channel.QueueBind("attika_queue", "attika_exchange", "");

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, e) =>
            {
                _arrival(e.Body);
                _channel.BasicAck(e.DeliveryTag, false);
            };
            _channel.BasicConsume("attika_queue", true, consumer);
        }

        public void RegisterConsumer(Action<byte[]> arrived)
        {
            _arrival += arrived;
        }

        public void PushMessage(byte[] message)
        {
            using (var connection = _factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("attika_exchange", ExchangeType.Fanout);
                channel.BasicPublish("attika_exchange", "", null, message);
            }
        }
    }
}