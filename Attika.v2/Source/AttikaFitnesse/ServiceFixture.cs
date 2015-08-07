using System;
using fitlibrary;

namespace Infotecs.Attika.AttikaFitnesse
{
    public class ServiceFixture : DoFixture
    {
        public void StartService()
        {
            AttikaFixture.StartService();
        }

        public void StopService()
        {
            AttikaFixture.StopService();
        }
    }
}
