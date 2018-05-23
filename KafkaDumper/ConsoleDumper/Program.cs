using System;
using Gobi.KafkaDumper;

namespace Gobi.ConsoleDumper
{
    internal class Program
    {
        
        private static void Main(string[] args)
        {
            if (args.Length < 1)
                PrintUsage();
            switch (args[0])
            {
                case "dump":
                    if (args.Length < 4)
                        PrintUsage();
                    Dump(args[1], args[2], args[3]);
                    break;
                case "load":
                    if (args.Length < 3)
                        PrintUsage();
                    Load(args[1], args[2]);
                    break;
                default:
                    PrintUsage();
                    return;
            }
        }

        private static void Dump(string brokers, string topic, string dumpFolder)
        {
            IKafkaReader kafkaReader = new KafkaReader(config => { config.BrokerList = brokers; });
            var dumper = new DumpExporter(kafkaReader);

            dumper.ExportAsync(topic, dumpFolder)
                .Wait();
        }

        private static void Load(string brokers, string dumpFolder)
        {
            IKafkaWriter pump = new KafkaWriter(config => { config.BrokerList = brokers; });
            var dumper = new DumpImporter(() => pump);

            dumper.Import(dumpFolder);
        }

        private static void PrintUsage()
        {
            Console.WriteLine("usage:");
            Console.WriteLine("console-dumper dump broker-list topic dump-folder");
            Console.WriteLine("console-dumper load broker-list dump-folder");
        }
    }
}