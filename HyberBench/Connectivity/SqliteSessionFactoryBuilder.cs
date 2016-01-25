using System.Data.SQLite;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;

namespace HyberBench.Connectivity
{
    public class SqliteSessionFactoryBuilder : ISessionFactoryBuilder
    {
        private SQLiteConnection _connection;
        private SchemaExport _schemaExport;

        public ISessionFactory Build()
        {
            var configuration = new Configuration();
            configuration.Properties.Add(Environment.Dialect, "NHibernate.Dialect.SQLiteDialect");
            configuration.Properties.Add(Environment.ConnectionString,
                @"FullUri=file:memorydb.db?mode=memory&cache=shared");
            configuration.Properties.Add(Environment.ReleaseConnections, "on_close");
            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());

            var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            configuration.AddMapping(mapping);


            var factory = configuration.BuildSessionFactory();
            _schemaExport = new SchemaExport(configuration);
            _connection = new SQLiteConnection(@"FullUri=file:memorydb.db?mode=memory&cache=shared");
            _connection.Open();
            _schemaExport.Execute(false, true, false, _connection, null);

            return factory;
        }

        public void RecreateDb()
        {
            _connection.Close();
            _connection.Open();
            _schemaExport.Execute(false, true, false, _connection, null);
        }
    }
}