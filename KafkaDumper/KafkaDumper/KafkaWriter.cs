using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;

namespace Gobi.KafkaDumper
{
    public class KafkaWriter : IKafkaWriter
    {
        private readonly Producer<string, byte[]> _producer;

        public KafkaWriter(Action<KafkaWriterConfig> configurator)
        {
            var kafkaWriterConfig = new KafkaWriterConfig();
            configurator(kafkaWriterConfig);
            kafkaWriterConfig.ProducerConfig["bootstrap.servers"] = kafkaWriterConfig.BrokerList;
            _producer = new Producer<string, byte[]>(kafkaWriterConfig.ProducerConfig,
                new StringSerializer(Encoding.UTF8),
                new ByteArraySerializer());
        }

        public void Dispose()
        {
            Flush();
            _producer?.Dispose();
        }

        public async Task PublishAsync(string topic, string key, byte[] message)
        {
            await _producer.ProduceAsync(topic, key, message);
        }

        private void Flush()
        {
            _producer.Flush(TimeSpan.FromMinutes(1));
        }

        public class KafkaWriterConfig
        {
            public KafkaWriterConfig()
            {
                ProducerConfig = DefaultProducerConfig;
            }

            private Dictionary<string, object> DefaultProducerConfig => new Dictionary<string, object>
            {
                {"bootstrap.servers", BrokerList},
                {"queue.buffering.max.ms", 1},
                {"socket.keepalive.enable", true}
            };

            public Dictionary<string, object> ProducerConfig { get; set; }
            public string BrokerList { get; set; } = "127.0.0.1:9092";
        }
    }
}