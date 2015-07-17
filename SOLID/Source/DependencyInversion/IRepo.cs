namespace Infotecs.SOLID.DependencyInversion
{
    public interface IRepo
    {
        IPerson GetPersonByName(string name);
    }
}