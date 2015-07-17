namespace Infotecs.SOLID.DependencyInversion
{
    internal sealed class FakeRepo : IRepo
    {
        public IPerson GetPersonByName(string name)
        {
            return new Person(name);
        }
    }
}