using RepoFac.Repo;

namespace RepoFac.UnitOfWork
{
    public class UnitOfMock : IUnitOfMock
    {
        private readonly IRepoFactory _repoFactory;

        public UnitOfMock(IRepoFactory repoFactory)
        {
            _repoFactory = repoFactory;
        }

        public T Repo<T>(ISession session) where T : class, IRepo
        {
            return _repoFactory.Get<T>(session);
        }
    }
}