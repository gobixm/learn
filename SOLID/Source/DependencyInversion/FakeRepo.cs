namespace Infotecs.SOLID.DependencyInversion
{
    internal class FakeRepo : IRepo
    {
        public IPerson GetPersonByName(string name)
        {
            return new Person(name);
        }
    }
}