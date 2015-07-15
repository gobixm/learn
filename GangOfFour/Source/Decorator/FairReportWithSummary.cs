using System;

namespace Infotecs.GangOfFour.Decorator
{
    internal class FairReportWithSummary : IReportWithSummary
    {
        public FairReportWithSummary(IReport report)
        {
            Report = report;
        }

        public IReport Report { get; private set; }

        public void Print()
        {
            Report.Print();
            Console.WriteLine("Fair summary info");
        }
    }
}