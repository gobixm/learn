using System;
using Infotecs.Opus;
using NLog;

namespace Infotecs.Attika.AttikaConsoleHost
{
    public class Environment
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void DumpInfo()
        {
            IConfiguration dumper = OpusDumper.Configure(x =>
            {
                x.Include(InformationKind.ApplicationInfo);
                x.Include(InformationKind.NetFrameworkInstalled);
                x.Include(InformationKind.OperatingSystem);
            });

            Logger.Info(dumper.Dump());
        }
    }
}
