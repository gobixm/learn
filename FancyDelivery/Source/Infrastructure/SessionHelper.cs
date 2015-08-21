using System;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Infrastructure
{
    public static class SessionHelper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    var configuration = new Configuration();
                    configuration.Configure();
                    configuration.Configure();
                    configuration.AddAssembly(typeof(Product).Assembly);
                    _sessionFactory = configuration.BuildSessionFactory();
                    return _sessionFactory;
                }
                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

        public static void PrepareDatabase()
        {
            var configuration = new Configuration();
            configuration.Configure();
            new SchemaUpdate(configuration).Execute(true, true);
        }
    }
}
