using NHibernate;

namespace HyberBench.Connectivity
{
    public interface ISessionHelper
    {
        ISession Session { get; }
        IStatelessSession StatelessSession { get; }
        IUnitOfWork UnitOfWork(ISession session);
    }
}