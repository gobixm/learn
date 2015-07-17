using System;

namespace Infotecs.SOLID.SingeResponsibility
{
    internal static class HeaderFormatter
    {
        public static Report FormatHeader(Report report, string header)
        {
            return new Report("Caption: " + header + " " + DateTime.Now, report.Body);
        }
    }
}