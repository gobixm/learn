using System;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;

namespace Infotecs.Attika.AttikaInfrastructure.Data
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
                    HbmMapping mapping = GetMappings();
                    configuration.AddDeserializedMapping(mapping, null);
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
            HbmMapping mapping = GetMappings();
            configuration.AddDeserializedMapping(mapping, null);
            new SchemaUpdate(configuration).Execute(true, true);
        }

        private static HbmMapping GetMappings()
        {
            var mapper = new ModelMapper();
            mapper.AddMappings(typeof(SessionHelper).Assembly.GetExportedTypes());
            HbmMapping mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            return mapping;
        }
    }
}
