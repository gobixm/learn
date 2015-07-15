using System;

namespace Infotecs.GangOfFour.Bridge
{
    internal interface IKeyGenerator
    {
        IKeyGeneratorImplementation Implementation { get; set; }
        string GenerateKey();
    }
}
