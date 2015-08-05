using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Infotecs.Attika.AttikaInfrastructure.Services.Contracts;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Framing;

namespace Infotecs.Attika.AttikaDomain.Services.Queuing
{
    public sealed class QueueService : IQueueService
    {
        private const string AttikaExchange = "attika_exchange";

        private static readonly Configuration InternalConfiguration = new Configuration();

        private static readonly Dictionary<Type, IQueueProcessor> MessageProcessors =
            new Dictionary<Type, IQueueProcessor>();

        private static readonly Dictionary<string, Type> MessageTypes = new Dictionary<string, Type>();
        private readonly IMessageSerializationService _messageSerializationService;

        private static IModel _channel;
        private static IConnection _connection;
        private static ConnectionFactory _factory;
        private bool _disposed;

        public QueueService(IMessageSerializationService messageSerializationService)
        {
            _messageSerializationService = messageSerializationService;
            EnshureMessageProcessingEnabled();
        }

        ~QueueService()
        {
            Dispose(false);
        }

        public static IConfiguration Configure(Action<IConfiguration> action)
        {
            action(InternalConfiguration);
            return InternalConfiguration;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void PushMessage(object message)
        {
            using (IConnection connection = _factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(AttikaExchange, ExchangeType.Fanout);
                    var properties = new BasicProperties
                    {
                        Headers = new Dictionary<string, object> { { "type", message.GetType().Name } },
                        Persistent = true
                    };
                    channel.BasicPublish(AttikaExchange, "", properties, _messageSerializationService.Serialize(message));
                }
            }
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_channel != null)
                    {
                        _channel.Dispose();
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                    }
                }
                _disposed = true;
            }
        }

        private void EnshureMessageProcessingEnabled()
        {
            if (_factory == null)
            {
                ConnectionStringSettings host = ConfigurationManager.ConnectionStrings["rabbit"];
                string hostname = "localhost";
                if (host != null)
                {
                    hostname = host.ConnectionString;
                }

                _factory = new ConnectionFactory
                {
                    HostName = hostname
                };

                _connection = _factory.CreateConnection();

                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare(AttikaExchange, ExchangeType.Fanout);
                _channel.QueueDeclare("attika_queue", true, false, false, null);
                _channel.QueueBind("attika_queue", AttikaExchange, "");

                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += (model, e) =>
                {
                    InvokeProcessor(e);
                    _channel.BasicAck(e.DeliveryTag, false);
                };
                _channel.BasicConsume("attika_queue", false, consumer);
            }
        }

        private void InvokeProcessor(BasicDeliverEventArgs e)
        {
            if (e.BasicProperties.Headers.ContainsKey("type") == false)
            {
                return;
            }
            string messageTypeName = Encoding.UTF8.GetString((byte[])e.BasicProperties.Headers["type"]);
            if (MessageTypes.ContainsKey(messageTypeName))
            {
                Type messageType = MessageTypes[messageTypeName];
                if (MessageProcessors.ContainsKey(messageType))
                {
                    IQueueProcessor processor = MessageProcessors[messageType];
                    object message = _messageSerializationService.Deseriallize(e.Body, messageType);
                    ((dynamic)processor).Process((dynamic)message);
                }
            }
        }

        private sealed class Configuration : IConfiguration
        {
            public void Bind<TMessage, TProcessor>(Func<TProcessor> creator)
                where TMessage : class
                where TProcessor : IQueueProcessor
            {
                if (creator == null)
                {
                    throw new ArgumentNullException("creator");
                }
                MessageProcessors.Add(typeof(TMessage), creator());
                MessageTypes.Add(typeof(TMessage).Name, typeof(TMessage));
            }
        }
    }
}
