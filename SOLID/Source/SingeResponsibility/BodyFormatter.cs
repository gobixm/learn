namespace Infotecs.SOLID.SingeResponsibility
{
    internal static class BodyFormatter
    {
        public static Report FormatBody(Report report)
        {
            string[] words = ReportSplitter.Split(report);
            return new Report(report.Header, string.Join(" ", RedundantInserter.InsertYouKnow(words)));
        }
    }
}