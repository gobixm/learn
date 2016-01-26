using RepoFac.Repo;

namespace RepoFac.UnitOfWork
{
    public interface IUnitOfMock
    {
        T Repo<T>(ISession session) where T : class, IRepo;
    }
}