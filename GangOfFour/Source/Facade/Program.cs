using System;
using Microsoft.CSharp.RuntimeBinder;

namespace Infotecs.GangOfFour.Facade
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                ReservationHelper.ReserveRoom("Joe",
                    "25-2",
                    DateTime.Now,
                    DateTime.Now+TimeSpan.FromDays(1));
            }
            catch (RuntimeBinderException)
            {
                Console.WriteLine("it's just example");
                throw;
            }
            Console.ReadKey();
        }
    }
}
