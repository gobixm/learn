using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace Infotecs.GangOfFour.Iterator
{
    internal sealed class ColorCollection : IEnumerable<Color>
    {
        private readonly List<Color> _list = new List<Color>();

        public ColorCollection(IEnumerable<Color> colors)
        {
            _list.AddRange(colors);
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
