using System;
using Confluent.Kafka;

namespace Gobi.KafkaDumper
{
    public interface IKafkaReader
    {
        void ReadToEnd(string topic, Action<Message<string, byte[]>> received);
    }
}