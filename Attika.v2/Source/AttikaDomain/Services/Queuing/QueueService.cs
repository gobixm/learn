using System;
using System.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Framing;

namespace Infotecs.Attika.AttikaDomain.Services.Queuing
{
    public sealed class QueueService : IQueueService, IDisposable
    {
        private IModel _channel;
        private IConnection _connection;
        private bool _disposed;
        private ConnectionFactory _factory;

        public void RegisterConsumer(EventHandler<byte[]> arrived)
        {
            Arrival += arrived;
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void UnregisterConsumer(EventHandler<byte[]> arrived)
        {
            Arrival -= arrived;
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (_channel != null)
                    _channel.Dispose();
                if (_connection != null)
                    _connection.Dispose();
                _disposed = true;
            }
        }

        ~QueueService()
        {
            Dispose(false);
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

                _connection = _factory.CreateConnection();

                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare("attika_exchange", ExchangeType.Fanout);
                _channel.QueueDeclare("attika_queue", true, false, false, null);
                _channel.QueueBind("attika_queue", "attika_exchange", "");

                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += (model, e) =>
                    {
                        Arrival(this, e.Body);
                        _channel.BasicAck(e.DeliveryTag, false);
                    };
                _channel.BasicConsume("attika_queue", false, consumer);
            }
        }

        public event EventHandler<byte[]> Arrival = (sender, bytes) => { };
    }
}