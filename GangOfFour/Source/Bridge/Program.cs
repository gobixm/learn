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
            IKeyGenerator fakeGenerator = new StandardKeyGenerator(fakeImplementation);

            Console.WriteLine(keyGenerator.GenerateKey());
            Console.WriteLine(fakeGenerator.GenerateKey());
            Console.ReadKey();
        }
    }
}
