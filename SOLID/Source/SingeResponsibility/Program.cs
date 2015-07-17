using System;

namespace Infotecs.SOLID.SingeResponsibility
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var report = new Report("", "It's very impressive report");
            var formatter = new ReportFormatter(report);
            ReportPrinter.Print(formatter.Format("first caption"));
            ReportPrinter.Print(formatter.Format("second caption"));
            Console.ReadKey();
        }
    }
}