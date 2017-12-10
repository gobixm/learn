using System;
using System.IO;
using System.Text;
using ProtoBuf;

namespace Gobi.KafkaDumper
{
    public sealed class DumpImporter
    {
        private readonly Func<IKafkaWriter> _kafkaWriterFactory;

        public DumpImporter(Func<IKafkaWriter> kafkaWriterFactory)
        {
            _kafkaWriterFactory = kafkaWriterFactory;
        }

        public void Import(string dumpFolder)
        {
            if (!Directory.Exists(dumpFolder))
                throw new FileNotFoundException("Folder not found", dumpFolder);
            foreach (var file in Directory.GetFiles(dumpFolder, "*.topic"))
            {
                var topicName = Path.GetFileNameWithoutExtension(file);
                using (var stream = File.OpenRead(file))
                {
                    using (var kafkaWriter = _kafkaWriterFactory())
                    {
                        while (true)
                        {
                            var message =
                                Serializer.DeserializeWithLengthPrefix<MessageDto>(stream, PrefixStyle.Fixed32);
                            if (message == null)
                                break;
                            kafkaWriter.PublishAsync(topicName, message.Key, message.Value);
                        }
                    }
                }
            }
        }
    }
}