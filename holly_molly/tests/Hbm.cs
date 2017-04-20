using System.Linq;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Linq;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using tests.entities;

namespace tests
{
    public class Hbm
    {


        private readonly Configuration _config = new Configuration();
        private ISessionFactory _sessionFactory;

        [OneTimeSetUp]
        public void Init()
        {
            _config.DataBaseIntegration(db =>
            {
                db.ConnectionString =
                    @"User ID=postgres;Password=11111111;Host=localhost;Port=5432;Database=holly;Pooling=true;";
                db.Dialect<PostgreSQL82Dialect>();
                db.LogSqlInConsole = true;
            });
            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());
            var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            _config.AddMapping(mapping);
        }

        [SetUp]
        public void Setup()
        {

            new SchemaExport(_config).Execute(true, true, false);
            _sessionFactory = _config.BuildSessionFactory();
        }

        [Test]
        public void QuerySublink()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var user1 = new User { Name = "name1" };
                var user2 = new User { Name = "name2" };
                var user3 = new User { Name = "name3" };
                var user4 = new User { Name = "name4" };

                var node1 = new Node { Name = "node1" };
                var node2 = new Node { Name = "node2" };

                session.Save(user1);
                session.Save(user2);
                session.Save(user3);
                session.Save(user4);
                session.Save(node1);
                session.Save(node2);

                var userLink1 = new UserLink (user1, user2);
                var userLink2 = new UserLink (user1, user3);
                var userLink3 = new UserLink (user1, user4);
                var nodeLink = new NodeLink { Node1 = node1, Node2 = node2 };
                session.Save(userLink1);
                session.Save(userLink2);
                session.Save(userLink3);
                session.Save(nodeLink);
                session.Flush();
            }

            using (var session = _sessionFactory.OpenSession())
            {
                var userLinks = session.Query<UserLink>().Where(x => ((User)x.Entity2).Name == "name2" || ((User)x.Entity2).Name == "name4")
                    .Fetch(x => x.Entity2)
                    .ToList();
                var links = session.Query<Link>()
                    .Where(x => ((User)x.Entity1).Name == "name1")
                    .Fetch(x => x.Entity1)
                    .Fetch(x => x.Entity2)
                    .ToList();
                Assert.AreEqual(2, userLinks.Count);
                Assert.AreEqual(3, links.Count);
            }
        }
    }
}