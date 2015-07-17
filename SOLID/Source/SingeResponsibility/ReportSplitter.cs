namespace Infotecs.SOLID.SingeResponsibility
{
    internal static class ReportSplitter
    {
        public static string[] Split(Report report)
        {
            return report.Body.Split(' ');
        }
    }
}