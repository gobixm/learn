using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Infotecs.Attika.AttikaSharedDataObjects.Messages;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infotecs.Attika.AttikaQueueProcessor.Processors
{
    public sealed class QueueDirector
    {
        private readonly ConnectionFactory _factory;
        private readonly Dictionary<string, BaseProcessor> _processors;
        private readonly Dictionary<string, Type> _types;
        private IModel _channel;
        private IConnection _connection;

        public QueueDirector(IEnumerable<BaseProcessor> processors)
        {
            _processors = new Dictionary<string, BaseProcessor>();
            _types = new Dictionary<string, Type>();
            _factory = new ConnectionFactory {HostName = "localhost"};
            foreach (BaseProcessor processor in processors)
            {
                RegisterProcessor(processor);
            }
        }

        public void RegisterProcessor(BaseProcessor processor)
        {
            Type processorType = processor.GetType();
            if (processorType.Name.EndsWith("Processor"))
            {
                string processorName = processorType.Name.Replace("Processor", "");
                IEnumerable<MethodInfo> methods = from m in processorType.GetMethods()
                                                  where m.Name == "Process"
                                                  select m;
                foreach (MethodInfo method in methods)
                {
                    ParameterInfo[] parameters = method.GetParameters();
                    if (parameters.Length == 1)
                    {
                        string typeName = processorName + "." + parameters[0].ParameterType.Name;
                        _processors.Add(typeName, processor);
                        _types.Add(typeName, parameters[0].ParameterType);
                    }
                }
            }
        }

        public void ProcessMessage(string message)
        {
            Console.WriteLine("incoming: " + message);
            JsonReader jsonReader = new JsonTextReader(new StringReader(message));
            string messageHeader = "";
            while (jsonReader.Read())
            {
                if ((jsonReader.TokenType == JsonToken.PropertyName) && (jsonReader.Value.ToString() == "Request"))
                {
                    jsonReader.Read();
                    messageHeader = jsonReader.Value.ToString();
                    break;
                }
            }
            if (messageHeader.Length > 0)
            {
                BaseProcessor processor = GetProcessor(messageHeader);
                if (processor != null)
                {
                    Type messageType = GetMessageType(messageHeader);
                    if (messageType != null)
                    {
                        processor.Process((BaseMessage) JsonConvert.DeserializeObject(message, messageType));
                    }
                }
            }
        }

        private BaseProcessor GetProcessor(string messageHeader)
        {
            return _processors.ContainsKey(messageHeader) ? _processors[messageHeader] : null;
        }

        private Type GetMessageType(string messageHeader)
        {
            return _types.ContainsKey(messageHeader) ? _types[messageHeader] : null;
        }

        public void StartManagement()
        {
            _connection = _factory.CreateConnection();

            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare("attika_exchange", ExchangeType.Fanout);
            _channel.QueueDeclare("attika_queue", true, false, false, null);
            _channel.QueueBind("attika_queue", "attika_exchange", "");

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, e) =>
                {
                    ProcessMessage(Encoding.UTF8.GetString(e.Body));
                    _channel.BasicAck(e.DeliveryTag, false);
                };
            _channel.BasicConsume("attika_queue", true, consumer);
        }
    }
}