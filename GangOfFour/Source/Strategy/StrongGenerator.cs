using System;

namespace Infotecs.GangOfFour.Strategy
{
    public sealed class StrongGenerator:IGenerator
    {

        public string Generate()
        {
            return Guid.NewGuid().ToString();
        }
    }
}