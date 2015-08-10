using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using Infotecs.Attika.AttikaConsoleHost.Configurations.ApplicationRoots;
using Infotecs.Opus;
using NLog;
using Nelibur.ServiceModel.Services.Default;

namespace Infotecs.Attika.AttikaConsoleHost
{
    public class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private static void Main(string[] args)
        {
            IConfiguration dumper = OpusDumper.Configure((x) =>
            {
                x.Include(InformationKind.ApplicationInfo);
                x.Include(InformationKind.NetFrameworkInstalled);
                x.Include(InformationKind.OperatingSystem);
            });

            Logger.Info(dumper.Dump());

            using (NinjectServiceLocator.Kernel)
            {
                Console.WriteLine("Message based service");
                var service = new WebServiceHost(typeof(JsonServicePerCall));
                try
                {
                    service.Open();
                }
                catch (AddressAccessDeniedException)
                {
                    Console.WriteLine("Access denied. Run as administrator");
                    return;
                }
                Console.WriteLine("Service started. Press any key to stop service.");
                Console.ReadKey();
                service.Close();
            }
        }
    }
}
