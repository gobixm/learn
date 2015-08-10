using System;
using System.ServiceModel.Web;
using Infotecs.Attika.AttikaConsoleHost.Configurations.ApplicationRoots;
using Nelibur.ServiceModel.Services.Default;
using dbfit;

namespace Infotecs.Attika.AttikaFitnesse
{
    public partial class AttikaFixture : SqlServerTest
    {
        public AttikaFixture()
        {
            NinjectServiceLocator.ForceInit();
            Service = new WebServiceHost(typeof(JsonServicePerCall));
        }

        public static WebServiceHost Service { get; set; }

        public static void StartService()
        {
            Service.Open();
        }

        public static void StopService()
        {
            Service.Close();
        }
    }
}
