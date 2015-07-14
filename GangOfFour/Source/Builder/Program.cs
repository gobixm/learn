using System;

namespace Infotecs.GangOfFour.Builder
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var carBuilder = new FancyCarBuilder();
            var director = new StandardCarDirector(carBuilder);
            director.Construct();
            FancyCar theCar = carBuilder.GetResult();
            Console.WriteLine("The car was constructed!: {0}", theCar);

            carBuilder = new FancyCarBuilder();
            var crazyDirector = new CrazyCarDirector(carBuilder);
            crazyDirector.Construct();
            theCar = carBuilder.GetResult();
            Console.WriteLine("The car was constructed!: {0}", theCar);

            Console.ReadKey();
        }
    }
}
