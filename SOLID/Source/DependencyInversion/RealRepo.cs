using System.Collections.Generic;

namespace Infotecs.SOLID.DependencyInversion
{
    internal sealed class RealRepo : IRepo
    {
        public RealRepo()
        {
            Persons = new Dictionary<string, IPerson>
                {
                    {"First", new Person("First")},
                    {"Second", new Person("Second")},
                    {"Third", new Person("Third")},
                };
        }

        private Dictionary<string, IPerson> Persons { get; set; }

        public IPerson GetPersonByName(string name)
        {
            return Persons[name];
        }
    }
}