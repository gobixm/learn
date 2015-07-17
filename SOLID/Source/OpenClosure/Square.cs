using System;

namespace Infotecs.SOLID.OpenClosure
{
    internal sealed class Square : IDrawable
    {
        public void Draw(int x)
        {
            Console.WriteLine("Square drawed at {0}", x);
        }
    }
}