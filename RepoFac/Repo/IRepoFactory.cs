using RepoFac.UnitOfWork;

namespace RepoFac.Repo
{
    public interface IRepoFactory
    {
        T Get<T>(ISession session) where T : class, IRepo;
    }
}