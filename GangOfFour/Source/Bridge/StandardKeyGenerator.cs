using System;

namespace Infotecs.GangOfFour.Bridge
{
    internal class StandardKeyGenerator : IKeyGenerator
    {
        public StandardKeyGenerator(IKeyGeneratorImplementation imp)
        {
            Implementation = imp;
        }

        public IKeyGeneratorImplementation Implementation { get; private set; }

        public string GenerateKey()
        {
            return Implementation.GenerateKey();
        }
    }
}
