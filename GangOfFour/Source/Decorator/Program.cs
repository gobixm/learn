using System;

namespace Infotecs.GangOfFour.Decorator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Report report = new Report();
            FairReportWithSummary fairReport = new FairReportWithSummary(report);
            UnfairReportWithSummary unfairReport = new UnfairReportWithSummary(report);
            fairReport.Print();
            Console.WriteLine();
            unfairReport.Print();
            Console.ReadKey();
        }
    }
}
