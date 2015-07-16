using System;

namespace Infotecs.GangOfFour.Observer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var sensor = new EarthquakeSensor<Message>();
            sensor.Attach(new StandardObserver<Message>("Berlin"));
            sensor.Attach(new StandardObserver<Message>("London"));
            sensor.Attach(new StandardObserver<Message>("Moscow"));
            sensor.Notify();
            Console.ReadKey();
        }
    }
}
