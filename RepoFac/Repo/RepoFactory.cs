using System;
using System.Collections.Generic;
using RepoFac.UnitOfWork;

namespace RepoFac.Repo
{
    public class RepoFactory : IRepoFactory
    {
        private readonly Configuration _configuration;

        public RepoFactory(Action<Configuration> configure)
        {
            _configuration = new Configuration(this);
            configure(_configuration);
        }

        public T Get<T>(ISession session) where T : class, IRepo
        {
            return _configuration.Get<T>(session);
        }

        public class Configuration
        {
            private readonly Dictionary<Type, Func<ISession, IRepo>> _repos = new Dictionary<Type, Func<ISession, IRepo>>();
            private RepoFactory _repoFactory;

            public Configuration(RepoFactory repoFactory)
            {
                _repoFactory = repoFactory;
            }

            public T Get<T>(ISession session) where T : class, IRepo
            {
                return _repos[typeof (T)](session) as T;
            }

            public void Bind<T>(Func<ISession, IRepo> repo)
            {
                _repos.Add(typeof (T), repo);
            }
        }
    }
}