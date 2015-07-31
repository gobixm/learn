using System;
using System.Linq;
using System.ServiceModel;
using Infotecs.Attika.AtticaDataModel;
using Infotecs.Attika.AttikaService;

namespace Infotecs.Attika.AttikaConsoleHost
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            SessionHelper.PrepareDatabase();

            ServiceHost service;

            if ((from a in args where a == "-classic" select a).Any())
            {
                Console.WriteLine("Classic service");
                service = new ServiceHost(typeof (ArticleService));
            }
            else
            {
                Console.WriteLine("Message based service");
                service = new ServiceHost(typeof (ArticleApiService));
            }
            service.Open();
            Console.WriteLine("Service started. Press any key to stop service.");
            Console.ReadKey();
            service.Close();
        }
    }
}