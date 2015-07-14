using System;

namespace Infotecs.GangOfFour.Builder
{
    /// <summary>
    /// Builds car with four wheels and one body
    /// </summary>
    internal class StandardCarDirector : ICarDirector
    {
        private readonly ICarBuilder _carBuilder;

        public StandardCarDirector(ICarBuilder builder)
        {
            _carBuilder = builder;
        }

        public void Construct()
        {
            _carBuilder
                .AddBody(new RoundBody())
                .AddWheel(new OctoWheel())
                .AddWheel(new OctoWheel())
                .AddWheel(new OctoWheel())
                .AddWheel(new OctoWheel());
        }
    }
}
