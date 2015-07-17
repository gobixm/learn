using System;

namespace Infotecs.SOLID.OpenClosure
{
    internal sealed class Circle : IDrawable
    {
        public void Draw(int x)
        {
            Console.WriteLine("Circle drawed at {0}", x);
        }
    }
}