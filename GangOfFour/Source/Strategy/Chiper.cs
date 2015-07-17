using System;

namespace Infotecs.GangOfFour.Strategy
{
    internal sealed class Chiper
    {
        private IGenerator _generator;

        public Chiper(IGenerator generator)
        {
            _generator = generator;
        }

        public IGenerator Generator
        {
            get { return _generator; }
            set { _generator = value; }
        }

        public void Encode(string content)
        {
            Console.WriteLine("{0} endcoded with key {1}", content, _generator.Generate());
        }
    }
}
