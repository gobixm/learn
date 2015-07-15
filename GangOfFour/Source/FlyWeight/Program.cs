using System;
using System.Collections.Generic;

namespace Infotecs.GangOfFour.FlyWeight
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IEmoticonFactory sharedEmoticonFactory = new SharedEmoticonFactory();
            var icons = new List<IEmoticon>
            {
                sharedEmoticonFactory.GetEmoticon("first"),
                sharedEmoticonFactory.GetEmoticon("second"),
                sharedEmoticonFactory.GetEmoticon("third"),
                sharedEmoticonFactory.GetEmoticon("first")
            };
            int i = 16;
            foreach (IEmoticon emoticon in icons)
            {
                emoticon.Draw(i += i, i);
            }
            if (ReferenceEquals(icons[0], icons[3]))
            {
                Console.WriteLine("icon[0] and icon[3] are the same instance");
            }
            Console.ReadKey();
        }
    }
}
