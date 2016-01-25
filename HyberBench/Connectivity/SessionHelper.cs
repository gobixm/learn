using Autofac;
using NHibernate;

namespace HyberBench.Connectivity
{
    public class SessionHelper : ISessionHelper
    {
        private readonly IComponentContext _componentContext;

        public SessionHelper(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public IUnitOfWork UnitOfWork(ISession session)
        {
            return _componentContext.Resolve<IUnitOfWork>(new PositionalParameter(0, session));
        }

        public ISession Session => _componentContext.Resolve<ISession>();
        public IStatelessSession StatelessSession => _componentContext.Resolve<IStatelessSession>();
    }
}