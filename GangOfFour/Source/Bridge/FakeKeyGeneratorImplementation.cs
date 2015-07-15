using System;

namespace Infotecs.GangOfFour.Bridge
{
    internal class FakeKeyGeneratorImplementation : IKeyGeneratorImplementation
    {
        public string GenerateKey()
        {
            return "fake key";
        }
    }
}
