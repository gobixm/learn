using System;
using System.Collections.Generic;

namespace Infotecs.GangOfFour.Composite
{
    internal class Employee : IUnit
    {
        public string Address { get; set; }

        public IList<IUnit> SubUnits { get; set; }

        public void Move(string address)
        {
            Address = address;
        }

        public void Print(int indent)
        {
            Console.WriteLine("".PadLeft(indent) + GetType().Name + "->" + Address);
        }
    }
}
