using System;
using System.Collections.Generic;
using System.Drawing;

namespace Infotecs.GangOfFour.Iterator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var colors = new ColorCollection(
                new List<Color> { Color.Black, Color.White, Color.Yellow, Color.Red });

            Console.WriteLine("Normal emunerator:");
            foreach (Color color in colors)
            {
                Console.WriteLine(color.Name);
            }
            Console.WriteLine("Racist emunerator:");
            IEnumerator<Color> enumerator = colors.GetRacistEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.Name);
            }
            Console.ReadKey();
        }
    }
}
