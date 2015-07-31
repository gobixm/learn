using System.Text;
using Infotecs.Attika.AttikaSharedDataObjects.Messages;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Infotecs.Attika.AttikaService.Messages.Queues
{
    public sealed class QueuePusher : IQueuePusher
    {
        private readonly ConnectionFactory _factory;

        public QueuePusher()
        {
            _factory = new ConnectionFactory {HostName = "localhost"};
        }

        public void PushMessage(BaseMessage message)
        {
            using (IConnection connection = _factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("attika_exchange", ExchangeType.Fanout);
                channel.BasicPublish("attika_exchange", "", null,
                                     Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message)));
            }
        }
    }
}