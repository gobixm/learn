using System;

namespace Infotecs.GangOfFour.Prototype
{
    internal class Request : IRequest
    {
        private readonly char[] _data;

        public Request(char[] data)
        {
            _data = data;
        }

        public char[] Data
        {
            get { return _data; }
        }

        public IRequest Clone()
        {
            return new Request((char[])_data.Clone());
        }

        public void Modify(Func<char, char> replace)
        {
            for (int i = 0; i < Data.Length; i++)
            {
                _data[i] = replace(_data[i]);
            }
        }
    }
}
