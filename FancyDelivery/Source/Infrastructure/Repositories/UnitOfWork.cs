using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ITransaction transaction;

        public ISession Session { get; private set; }

        public UnitOfWork()
        {
            Session = SessionHelper.OpenSession();
        }

        public void BeginTransaction()
        {
            transaction = Session.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                Session.Close();
            }
        }
    }
}
