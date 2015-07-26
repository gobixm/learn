using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Infotecs.Attika.AtticaDataModel
{
    public sealed class SessionHelper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory != null) return _sessionFactory;
                var configuration = new Configuration();
                configuration.Configure();
                configuration.AddAssembly(typeof (SessionHelper).Assembly);
                _sessionFactory = configuration.BuildSessionFactory();
                return _sessionFactory;
            }
        }

        internal static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

        public static void PrepareDatabase()
        {
            var configuration = new Configuration();
            configuration.Configure();
            configuration.AddAssembly(typeof (SessionHelper).Assembly);
            new SchemaUpdate(configuration).Execute(true, true);
        }
    }
}