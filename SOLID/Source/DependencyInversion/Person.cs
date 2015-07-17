using System;

namespace Infotecs.SOLID.DependencyInversion
{
    internal sealed class Person : IPerson
    {
        private static readonly Random Random = new Random();

        public Person(string name)
        {
            Name = name;
            Loan = (decimal) (Random.NextDouble()*100);
        }

        public decimal Loan { get; private set; }
        public string Name { get; private set; }
    }
}