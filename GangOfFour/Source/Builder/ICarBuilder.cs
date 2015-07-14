using System;

namespace Infotecs.GangOfFour.Builder
{
    internal interface ICarBuilder
    {
        ICarBuilder AddBody(IBody body);
        ICarBuilder AddWheel(IWheel wheel);
    }
}
