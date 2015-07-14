using System;

namespace Infotecs.GangOfFour.Builder
{
    internal abstract class AbstractCarDirector : ICarDirector
    {
        private readonly ICarBuilder _buider;

        protected AbstractCarDirector(ICarBuilder buider)
        {
            _buider = buider;
        }

        public ICarBuilder Buider
        {
            get { return _buider; }
        }

        public abstract void Construct();
    }
}
