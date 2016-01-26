using RepoFac.Entities;
using RepoFac.UnitOfWork;

namespace RepoFac.Repo
{
    public class FancyRepo : GenericRepo<IUser>, IFancyRepo
    {
        public FancyRepo(ISession session) : base(session)
        {
        }
    }
}