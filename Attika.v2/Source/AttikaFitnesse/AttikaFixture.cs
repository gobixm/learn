using System;
using System.Diagnostics;
using System.ServiceModel.Web;
using Infotecs.Attika.AttikaConsoleHost.Configurations.ApplicationRoots;
using Nelibur.ServiceModel.Services.Default;
using Ninject;
using dbfit;

namespace Infotecs.Attika.AttikaFitnesse
{
    public partial class AttikaFixture : SqlServerTest
    {
        public AttikaFixture()
        {
            IKernel kernel = NinjectServiceLocator.Kernel;
            Service = new WebServiceHost(typeof(JsonServicePerCall));
            Debugger.Log(0, "Info", "AttikaFixture constructed");
        }

        public static WebServiceHost Service { get; set; }

        public static void StartService()
        {
            Service.Open();
            Debugger.Log(0, "info", "service started");
        }

        public static void StopService()
        {
            Service.Close();
        }
    }
}
