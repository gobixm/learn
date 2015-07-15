using System;

namespace Infotecs.GangOfFour.Bridge
{
    public class HarwareKeyGeneratorImplementation : IKeyGeneratorImplementation
    {
        public string GenerateKey()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
