using System;

namespace Infotecs.GangOfFour.Decorator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var report = new Report();
            var fairReport = new FairReportWithSummary(report);
            var unfairReport = new UnfairReportWithSummary(report);
            fairReport.Print();
            Console.WriteLine();
            unfairReport.Print();
            Console.ReadKey();
        }
    }
}
