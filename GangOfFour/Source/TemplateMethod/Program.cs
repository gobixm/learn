using System;

namespace Infotecs.GangOfFour.TemplateMethod
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            StringRenderer stringRenderer = new RoundBracketRenderer();
            stringRenderer.Render("text");
            Console.ReadKey();
        }
    }
}
