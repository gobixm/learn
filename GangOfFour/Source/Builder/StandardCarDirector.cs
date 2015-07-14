using System;

namespace Infotecs.GangOfFour.Builder
{
    /// <summary>
    /// Builds car with four wheels and one body
    /// </summary>
    internal class StandardCarDirector : AbstractCarDirector
    {
        public StandardCarDirector(ICarBuilder builder) : base(builder)
        {
        }

        public override void Construct()
        {
            //standard car has body firstly, then four wheels
            Buider
                .AddBody(new RoundBody())
                .AddWheel(new OctoWheel())
                .AddWheel(new OctoWheel())
                .AddWheel(new OctoWheel())
                .AddWheel(new OctoWheel());
        }
    }
}
