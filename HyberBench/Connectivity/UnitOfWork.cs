using NHibernate;

namespace HyberBench.Connectivity
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ITransaction _transaction;

        public UnitOfWork(ISession session)
        {
            _transaction = session.BeginTransaction();
        }

        public void Dispose()
        {
            if (_transaction.IsActive)
            {
                _transaction.Rollback();
            }
        }

        public void Commit()
        {
            _transaction.Commit();
        }
    }
}