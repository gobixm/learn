using System;

namespace Infotecs.SOLID.DependencyInversion
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var realReport = new LoanReport(new RealRepo());
            Console.WriteLine("Real report:");
            realReport.Print();

            var fakeReport = new LoanReport(new FakeRepo());
            Console.WriteLine("Fake report:");
            fakeReport.Print();

            Console.ReadKey();
        }
    }
}