using RepoFac.Entities;
using RepoFac.UnitOfWork;

namespace RepoFac.Repo
{
    public class GenericRepo<T> : IGenericRepo<T> where T:IEntity
    {
        public GenericRepo(ISession session)
        {
        }

        public T GetAll()
        {
            return default(T);
        }

        public string WhoAmI()
        {
            return $"Im{GetType()}";
        }
    }
}