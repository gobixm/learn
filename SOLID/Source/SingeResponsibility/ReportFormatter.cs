namespace Infotecs.SOLID.SingeResponsibility
{
    internal class ReportFormatter
    {
        private readonly Report _report;

        public ReportFormatter(Report report)
        {
            _report = report;
        }

        public Report Format(string caption)
        {
            Report report = HeaderFormatter.FormatHeader(_report, caption);
            return BodyFormatter.FormatBody(report);
        }
    }
}