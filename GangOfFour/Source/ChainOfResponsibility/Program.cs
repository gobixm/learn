using System;

namespace Infotecs.GangOfFour.ChainOfResponsibility
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var handler = new StandardHandler<FirstRequest>(
                new StandardHandler<SecondRequest>(
                    new StandardHandler<ThirdRequest>(null)
                    )
                );

            handler.Handle(new FirstRequest());
            Console.WriteLine();
            handler.Handle(new SecondRequest());
            Console.WriteLine();
            handler.Handle(new ThirdRequest());
            Console.ReadKey();
        }
    }
}
