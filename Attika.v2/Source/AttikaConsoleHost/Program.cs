using System;
using System.ServiceModel;
using Infotecs.Attika.AttikaInfrastructure.Data;
using Infotecs.Attika.AttikaService;

namespace Infotecs.Attika.AttikaConsoleHost
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            SessionHelper.PrepareDatabase();

            Console.WriteLine("Message based service");
            var service = new ServiceHost(typeof (ArticleApiService));
            service.Open();
            Console.WriteLine("Service started. Press any key to stop service.");
            Console.ReadKey();
            service.Close();
        }
    }
}