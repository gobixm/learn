using System;

namespace Infotecs.GangOfFour.Strategy
{
    public sealed class WeakGenerator : IGenerator
    {
        public string Generate()
        {
            return DateTime.Now.ToString();
        }
    }
}
