namespace RepoFac.Repo
{
    public interface IGenericRepo<out T>:IRepo
    {
        T GetAll();
    }
}