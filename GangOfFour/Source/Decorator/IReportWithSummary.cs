namespace Infotecs.GangOfFour.Decorator
{
    public interface IReportWithSummary : IReport
    {
        IReport Report { get; }
    }
}