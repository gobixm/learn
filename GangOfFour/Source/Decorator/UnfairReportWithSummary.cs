using System;

namespace Infotecs.GangOfFour.Decorator
{
    internal class UnfairReportWithSummary : IReportWithSummary
    {
        public UnfairReportWithSummary(IReport report)
        {
            Report = report;
        }

        public IReport Report { get; private set; }

        public void Print()
        {
            Report.Print();
            Console.WriteLine("Unfair summary");
        }
    }
}
