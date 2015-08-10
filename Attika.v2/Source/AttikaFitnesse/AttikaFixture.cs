using System;
using System.Diagnostics;
using System.ServiceModel.Web;
using System.Threading;
using Infotecs.Attika.AttikaConsoleHost.Configurations.ApplicationRoots;
using Nelibur.ServiceModel.Services.Default;
using dbfit;

namespace Infotecs.Attika.AttikaFitnesse
{
    public class AttikaFixture : SqlServerTest
    {
        public AttikaFixture()
        {
            NinjectServiceLocator.ForceInit();
            Service = new WebServiceHost(typeof(JsonServicePerCall));
        }

        public static WebServiceHost Service { get; set; }

        public static void Debug()
        {
            Debugger.Launch();
        }

        public static void Sleep(int value)
        {
            Thread.Sleep(value);
        }

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
