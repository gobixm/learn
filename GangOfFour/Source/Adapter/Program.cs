using System;

namespace Infotecs.GangOfFour.Adapter
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var fancyControl = new ControlAdapter { Visible = false };
            ILibraryControl libraryControl = fancyControl;
            Console.WriteLine("Library callback: Visibility={0}", libraryControl.Visibility.ToString());
            (fancyControl as IFancyControl).Visible = true;
            Console.WriteLine("Library callback: Visibility={0}", libraryControl.Visibility.ToString());
            Console.ReadKey();
        }
    }
}
