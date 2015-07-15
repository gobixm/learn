using System;
using System.Collections.Generic;

namespace Infotecs.GangOfFour.Builder
{
    internal sealed class FancyCar
    {
        private readonly List<IBody> _bodys;
        private readonly List<IWheel> _wheels;

        public FancyCar(List<IWheel> wheels, List<IBody> bodys)
        {
            _wheels = wheels;
            _bodys = bodys;
        }

        public override string ToString()
        {
            return string.Format("The car with {0} wheels and {1} bodyes", _wheels.Count, _bodys.Count);
        }
    }
}
