using System;

namespace Infotecs.SOLID.DependencyInversion
{
    internal sealed class LoanReport
    {
        private readonly IRepo _repo;

        public LoanReport(IRepo repo)
        {
            _repo = repo;
        }

        public void Print()
        {
            var names = new[] {"First", "Second", "Third"};
            foreach (string name in names)
            {
                Console.WriteLine("Person {0} has {1} loan", name, _repo.GetPersonByName(name).Loan);
            }
            Console.WriteLine("----confirm----");
            foreach (string name in names)
            {
                Console.WriteLine("Person {0} has {1} loan", name, _repo.GetPersonByName(name).Loan);
            }
        }
    }
}