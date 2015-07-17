using System;

namespace Infotecs.SOLID.SingeResponsibility
{
    internal static class ReportPrinter
    {
        public static void Print(Report report)
        {
            Console.WriteLine("---------------------------");
            Console.WriteLine(report.Header);
            Console.WriteLine("---------------------------");
            Console.WriteLine(report.Body);
            Console.WriteLine("---------------------------");
        }
    }
}