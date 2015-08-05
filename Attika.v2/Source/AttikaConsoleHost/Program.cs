using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using Infotecs.Attika.AttikaConsoleHost.Configurations.ApplicationRoots;
using Infotecs.Attika.AttikaInfrastructure.Data;
using Nelibur.ServiceModel.Services.Default;

namespace Infotecs.Attika.AttikaConsoleHost
{
    public class Program
    {
        private static void Main(string[] args)
        {
            using (NinjectServiceLocator.Kernel)
            {
                SessionHelper.PrepareDatabase();

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
