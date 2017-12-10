using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;

namespace Gobi.KafkaDumper
{
    public sealed class KafkaReader : IKafkaReader
    {
        private readonly KafkaReaderConfig _kafkaReaderConfig;

        public KafkaReader(Action<KafkaReaderConfig> configurator)
        {
            _kafkaReaderConfig = new KafkaReaderConfig();
            configurator(_kafkaReaderConfig);
            _kafkaReaderConfig.ConsumerConfig["bootstrap.servers"] = _kafkaReaderConfig.BrokerList;
        }


        public void ReadToEnd(string topic, Action<Message<string, byte[]>> received)
        {
            using (var consumer = new Consumer<string, byte[]>(_kafkaReaderConfig.ConsumerConfig,
                new StringDeserializer(Encoding.UTF8),
                new ByteArrayDeserializer()))
            {
                var doneTokenSource = new CancellationTokenSource();
                var consumptionContext = new ConsumptionContext();

                consumer.OnPartitionEOF += (sender, offset) =>
                {
                    consumptionContext.Update(offset.Partition, ConsumptionContext.ConsumptionState.Eof);
                    if (consumptionContext.AllPartitionsConsumed())
                        doneTokenSource.Cancel();
                };

                consumer.OnError += (sender, error) => consumptionContext.SetError(error);
                consumer.OnConsumeError += (sender, message) => consumptionContext.SetError(message.Error);
                consumer.OnPartitionsAssigned += (sender, partitions) =>
                {
                    partitions.ForEach(x =>
                        consumptionContext.Update(x.Partition, ConsumptionContext.ConsumptionState.Consume));
                    consumer.Assign(partitions);
                };

                ConsumeLoop(topic, received, consumer, doneTokenSource, consumptionContext);
            }
        }

        private void ConsumeLoop(string topic,
            Action<Message<string, byte[]>> received,
            Consumer<string, byte[]> consumer,
            CancellationTokenSource doneTokenSource,
            ConsumptionContext consumptionContext)
        {
            consumer.Subscribe(new List<string> {topic});
            DateTimeOffset lastAlive = DateTime.Now;

            while (!doneTokenSource.Token.IsCancellationRequested)
            {
                if (DateTime.Now - lastAlive > _kafkaReaderConfig.KeepaliveTimeout)
                    throw new TimeoutException();
                if (!consumer.Consume(out var message, TimeSpan.FromMilliseconds(100)))
                    continue;
                lastAlive = DateTime.Now;
                consumptionContext.ThrowIfError();
                received(message);
            }
        }

        private class ConsumptionContext
        {
            public enum ConsumptionState
            {
                None = 0,
                Consume,
                Eof
            }

            private readonly ConcurrentDictionary<int, ConsumptionState> _partitionStates =
                new ConcurrentDictionary<int, ConsumptionState>();

            private Error _error;

            public void Update(int partition, ConsumptionState consumptionState)
            {
                _partitionStates[partition] = consumptionState;
            }

            public void SetError(Error error)
            {
                lock (this)
                {
                    _error = error;
                }
            }

            public bool AllPartitionsConsumed()
            {
                return _partitionStates.Values.All(x => x == ConsumptionState.Eof);
            }

            public void ThrowIfError()
            {
                lock (this)
                {
                    if (_error != null)
                        throw new Exception(_error.Reason);
                }
            }
        }

        public class KafkaReaderConfig
        {
            public KafkaReaderConfig()
            {
                ConsumerConfig = DefaultConsumerConfig;
            }

            private Dictionary<string, object> DefaultConsumerConfig => new Dictionary<string, object>
            {
                {"group.id", $"kafka-dumper-{Guid.NewGuid()}"},
                {"enable.auto.commit", false},
                {"bootstrap.servers", BrokerList},
                {
                    "default.topic.config", new Dictionary<string, object>
                    {
                        {"auto.offset.reset", "smallest"}
                    }
                }
            };

            public Dictionary<string, object> ConsumerConfig { get; set; }
            public TimeSpan KeepaliveTimeout { get; set; } = TimeSpan.FromMinutes(1);
            public string BrokerList { get; set; } = "127.0.0.1:9092";
        }
    }
}