using System;
using System.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Framing;

namespace Infotecs.Attika.AttikaDomain.Services.Queuing
{
    public sealed class QueueService : IQueueService
    {
        private Action<byte[]> _arrival;
        private IModel _channel;
        private IConnection _connection;
        private ConnectionFactory _factory;

        public void RegisterConsumer(Action<byte[]> arrived)
        {
            _arrival += arrived;
            EnshureMessageProcessingEnabled();
        }

        public void PushMessage(byte[] message)
        {
            using (IConnection connection = _factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("attika_exchange", ExchangeType.Fanout);
                var properties = new BasicProperties {Persistent = true};
                channel.BasicPublish("attika_exchange", "", properties, message);
            }
        }

        private void EnshureMessageProcessingEnabled()
        {
            if (_factory == null)
            {
                ConnectionStringSettings host = ConfigurationManager.ConnectionStrings["rabbit"];
                string hostname = "localhost";
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
                _channel.BasicConsume("attika_queue", false, consumer);
            }
        }
    }
}