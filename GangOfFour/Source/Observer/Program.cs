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
            Console.WriteLine("event");
            var evented = new Evented();
            evented.SimpleEvent += (sender, e) => Console.WriteLine("First subscriber");
            evented.SimpleEvent += (sender, e) => Console.WriteLine("Second subscriber");
            evented.Fire();
            Console.ReadKey();
        }
    }
}
