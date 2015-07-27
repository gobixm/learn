using System;
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
            using (var attika = new ServiceHost(typeof (ArticleService)))
            {
                attika.Open();
                Console.ReadKey();
                attika.Close();
            }
        }
    }
}