using System;

namespace Infotecs.GangOfFour.Builder
{
    internal class CrazyCarDirector : AbstractCarDirector
    {
        public CrazyCarDirector(ICarBuilder buider) : base(buider)
        {
        }

        public override void Construct()
        {
            Buider
                .AddBody(new RoundBody())
                .AddWheel(new OctoWheel())
                .AddWheel(new OctoWheel())
                .AddWheel(new OctoWheel())
                .AddWheel(new OctoWheel())
                .AddWheel(new OctoWheel())
                .AddBody(new RoundBody());
        }
    }
}
