using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace Infotecs.GangOfFour.Iterator
{
    internal class ColorCollection : IEnumerable<Color>
    {
        private readonly List<Color> _list = new List<Color>();

        public ColorCollection(IEnumerable<Color> colors)
        {
            _list.AddRange(colors);
        }

        public Color this[int index]
        {
            get { return _list[index]; }
            set { _list[index] = value; }
        }

        public IEnumerator<Color> GetRacistEnumerator()
        {
            foreach (Color color in _list)
            {
                if (color == Color.White)
                {
                    yield return color;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Color> GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }
}
