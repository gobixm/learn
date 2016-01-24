using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;

namespace HyberBench.Connectivity
{
    public class MsSqlSessionFactoryBuilder : ISessionFactoryBuilder
    {
        private SchemaExport _schemaExport;

        public ISessionFactory Build()
        {
            var configuration = new Configuration();
            configuration.Properties.Add(Environment.Dialect, "NHibernate.Dialect.MsSql2012Dialect");
            configuration.Properties.Add(Environment.ConnectionString,
                @"Data Source=.\sqlexpress;Initial Catalog=BenchDb;Integrated Security=True");
            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());

            var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            configuration.AddMapping(mapping);

            var factory = configuration.BuildSessionFactory();
            _schemaExport = new SchemaExport(configuration);
            _schemaExport.Execute(false, true, false);

            return factory;
        }

        public void RecreateDb()
        {
            _schemaExport.Execute(false, true, false);
        }
    }
}