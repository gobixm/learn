using System;
using System.Threading.Tasks;

namespace Gobi.KafkaDumper
{
    public interface IKafkaWriter : IDisposable
    {
        Task PublishAsync(string topic, string key, byte[] message);
    }
}