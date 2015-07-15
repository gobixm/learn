using System;

namespace Infotecs.GangOfFour.FactoryMethod
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ICreator milkCreator = new Creator<Milk>();

            ICreator breadCreator = new Creator<Bread>();
            breadCreator.CreateProduct("pure");
            Console.WriteLine(milkCreator
                .CreateProduct("human's")
                .Name);
            Console.WriteLine(milkCreator
                .CreateProduct("cow's")
                .Name);
            Console.WriteLine(breadCreator
                .CreateProduct("pure")
                .Name);
            Console.WriteLine(breadCreator
                .CreateProduct("infected")
                .Name);

            Console.ReadKey();
        }
    }
}
