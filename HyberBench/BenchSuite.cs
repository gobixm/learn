using System;
using System.Linq;
using HyberBench.Connectivity;
using HyberBench.Orm;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Util;

namespace HyberBench
{
    public class BenchSuite
    {
        private const int UserCount = 100;
        private const int GroupCount = 100;
        private const int DbCreateCount = 1000;
        private readonly ISessionFactoryBuilder _sessionFactoryBuilder;
        private readonly ISessionHelper _sessionHelper;

        public BenchSuite(ISessionHelper sessionHelper, ISessionFactoryBuilder sessionFactoryBuilder)
        {
            _sessionHelper = sessionHelper;
            _sessionFactoryBuilder = sessionFactoryBuilder;
        }

        public void Run()
        {
            Enumerable.Range(1, 2)
                .ForEach(i =>
                {
                    Console.WriteLine($"Pass {i}");
                    WarmUp();
                    InsertUsersWithTransaction();
                    InsertUsersWithoutTransaction();
                    SelectUsers();
                    UpdateGroups();
                    RecreateDb();
                });
        }

        private void RecreateDb()
        {
            Bench.Measure(nameof(RecreateDb), () =>
            {
                Enumerable.Range(1, DbCreateCount)
                    .ForEach(i => _sessionFactoryBuilder.RecreateDb());
            });
        }

        private void UpdateGroups()
        {
            Bench.Measure(nameof(UpdateGroups), () =>
            {
                using (var session = _sessionHelper.Session)
                {
                    using (var uow = _sessionHelper.UnitOfWork(session))
                    {
                        session.Query<Group>().ForEach(x =>
                        {
                            x.Name = $"{x.Name}_updated";
                            session.Save(x);
                        });
                        uow.Commit();
                    }
                }
            });
        }

        private void SelectUsers()
        {
            Bench.Measure(nameof(SelectUsers), () =>
            {
                using (var session = _sessionHelper.Session)
                {
                    using (var uow = _sessionHelper.UnitOfWork(session))
                    {
                        Enumerable.Range(1, UserCount)
                            .ForEach(i =>
                            {
                                var users =
                                    session.Query<User>()
                                        .Where(x => x.Name == $"user_{i}" && x.Groups.Any(y => y.Name == $"group_{i}_1"))
                                        .ToList();
                            });
                    }
                }
            });
        }

        private void WarmUp()
        {
            Bench.Measure(nameof(WarmUp), () =>
            {
                using (var session = _sessionHelper.Session)
                {
                    using (var uow = _sessionHelper.UnitOfWork(session))
                    {
                        var user = new User {Name = "test"};
                        var group = new Group {Name = "test"};
                        user.Groups.Add(group);
                        group.Users.Add(user);
                        session.Save(user);
                        session.Save(group);
                        session.Refresh(user);
                        session.Refresh(group);
                        var users = session.Query<User>().ToList().All(x => x.Name.Length > 0);
                        var groups = session.Query<Group>().ToList().All(x => x.Name.Length > 0);
                        session.Delete("from User");
                        session.Delete("from Group");
                        uow.Commit();
                    }
                }
            });
        }

        private void InsertUsersWithTransaction()
        {
            Bench.Measure(nameof(InsertUsersWithTransaction), () =>
            {
                using (var session = _sessionHelper.Session)
                {
                    using (var uow = _sessionHelper.UnitOfWork(session))
                    {
                        CreateUsers(session);
                        uow.Commit();
                    }
                }
            });
        }

        private static void CreateUsers(ISession session)
        {
            Enumerable.Range(1, UserCount)
                .ForEach(i =>
                {
                    var user = new User
                    {
                        Name = $"user_{i}"
                    };

                    Enumerable.Range(1, GroupCount)
                        .ForEach(j =>
                        {
                            var group = new Group
                            {
                                Name = $"group_{i}_{j}"
                            };
                            user.Groups.Add(group);
                            session.Save(group);
                        });
                    session.Save(user);
                });
        }

        private void InsertUsersWithoutTransaction()
        {
            Bench.Measure(nameof(InsertUsersWithoutTransaction), () =>
            {
                using (var session = _sessionHelper.Session)
                {
                    CreateUsers(session);
                    _sessionHelper.Session.Flush();
                }
            });
        }
    }
}