using System;
using System.ServiceModel;
using Infotecs.Attika.AttikaConsoleHost.Configurations.ApplicationRoots;
using Infotecs.Attika.AttikaInfrastructure.Data;
using Infotecs.Attika.AttikaService;

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
                var service = new ServiceHost(typeof (ArticleApiService));
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