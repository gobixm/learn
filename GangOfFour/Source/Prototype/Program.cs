using System;
using System.Threading.Tasks;

namespace Infotecs.GangOfFour.Prototype
{
    internal class Program
    {
        private const int ModifiersCount = 4;

        private static void Main(string[] args)
        {
            IRequest income = new Request("lorem ipsum etc.".ToCharArray());
            var res = new IRequest[ModifiersCount];
            Parallel.For(0, res.Length, (i) =>
            {
                IRequest copy = income.Clone();
                //there should be long and heavy operation
                copy.Modify(c => Convert.ToChar(Convert.ToInt32(c) + i));
                res[i] = copy;
            });

            //send modified requests to console
            foreach (IRequest request in res)
            {
                Console.WriteLine(new string(request.Data));
            }
            Console.ReadKey();
        }
    }
}
