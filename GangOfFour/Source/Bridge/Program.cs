using System;

namespace Infotecs.GangOfFour.Bridge
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IKeyGeneratorImplementation keyGeneratorImplementation = new HarwareKeyGeneratorImplementation();
            IKeyGeneratorImplementation fakeImplementation = new FakeKeyGeneratorImplementation();

            IKeyGenerator keyGenerator = new StandardKeyGenerator(keyGeneratorImplementation);

            Console.WriteLine(keyGenerator.GenerateKey());
            keyGenerator.Implementation = fakeImplementation;
            Console.WriteLine(keyGenerator.GenerateKey());
            Console.ReadKey();
        }
    }
}
