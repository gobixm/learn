using System;
using System.Threading.Tasks;

namespace Prototype
{
    internal class Program
    {
        private const int ModifierCount = 4;
        private static void Main(string[] args)
        {
            IRequest income = new Request("lorem ipsum etc.".ToCharArray());
            IRequest[] res = new IRequest[ModifierCount];
            Parallel.For(0, res.Length, (i) =>
            {
                var copy = income.Clone();
                //there should be long and heavy operation
                copy.Modify(c => Convert.ToChar(Convert.ToInt32(c) + i));
                res[i] = copy;
            });
            foreach (var request in res)
            {
                Console.WriteLine(new string(request.Data));
            }
            Console.ReadKey();
        }
    }
}
