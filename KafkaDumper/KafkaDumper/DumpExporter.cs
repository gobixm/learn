using System.IO;
using System.Threading.Tasks;
using Confluent.Kafka;
using ProtoBuf;

namespace Gobi.KafkaDumper
{
    public sealed class DumpExporter
    {
        private readonly IKafkaReader _kafkaReader;

        public DumpExporter(IKafkaReader kafkaReader)
        {
            _kafkaReader = kafkaReader;
        }

        public Task ExportAsync(string topic, string dumpFolder)
        {
            return Task.Run(() =>
            {
                Directory.CreateDirectory(dumpFolder);
                using (var stream = File.Create(Path.Combine(dumpFolder, $"{topic}.topic")))
                {
                    _kafkaReader.ReadToEnd(topic, message => { WriteMessage(message, stream); });
                }
            });
        }


        private static void WriteMessage(Message<string, byte[]> message, Stream stream)
        {
            var dto = new MessageDto
            {
                Key = message.Key,
                Value = message.Value
            };
            Serializer.SerializeWithLengthPrefix(stream, dto, PrefixStyle.Fixed32);
        }
    }
}