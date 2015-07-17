using System;

namespace Infotecs.SOLID.OpenClosure
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var circle = new Circle();
            var square = new Square();

            var renderList = new RenderList();
            renderList.AddDrawing(circle);
            renderList.AddDrawing(circle);
            renderList.AddDrawing(square);
            renderList.AddDrawing(square);
            renderList.AddDrawing(circle);
            renderList.AddDrawing(circle);
            renderList.Render();
            Console.ReadKey();
        }
    }
}