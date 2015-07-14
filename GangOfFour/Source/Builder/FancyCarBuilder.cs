using System;
using System.Collections.Generic;

namespace Infotecs.GangOfFour.Builder
{
    internal sealed class FancyCarBuilder : ICarBuilder
    {
        private readonly List<IBody> _bodys = new List<IBody>();
        private readonly List<IWheel> _wheels = new List<IWheel>();

        public FancyCar GetResult()
        {
            return new FancyCar(_wheels, _bodys);
        }

        public ICarBuilder AddBody(IBody body)
        {
            _bodys.Add(body);
            return this;
        }

        public ICarBuilder AddWheel(IWheel wheel)
        {
            _wheels.Add(wheel);
            return this;
        }
    }
}
