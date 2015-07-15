using System;

namespace Infotecs.GangOfFour.FlyWeight
{
    internal class SharedEmoticon : IEmoticon
    {
        public string FileName { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }
        //here must be huge memory consuming resource

        public void Draw(int width, int height)
        {
            Console.WriteLine("Share Emoticon {0} {1}x{2} drawed as {3}x{4}", FileName, Width, Height, width, height);
        }

        public void Load(string fileName)
        {
            Width = 256;
            Height = 256;
            FileName = fileName;
        }
    }
}
