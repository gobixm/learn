using System;

namespace Infotecs.GangOfFour.State
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var order = new Order();
            Console.WriteLine("good user story");
            order.Place();
            order.Pay();
            order.Ship();
            Console.WriteLine("bad user story");
            order = new Order();
            try
            {
                order.Pay();
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            try
            {
                order.Ship();
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            try
            {
                order.Place();
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            try
            {
                order.Ship();
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            try
            {
                order.Pay();
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }
    }
}
