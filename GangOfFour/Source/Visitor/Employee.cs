using System;
using System.Collections.Generic;

namespace Infotecs.GangOfFour.Visitor
{
    internal class Employee : IUnit
    {
        public Employee()
        {
            Address = "";
        }

        public string Address { get; set; }

        public IList<IUnit> SubUnits { get; set; }

        public void Accept(IVisitor visitor)
        {
            visitor.VisitEmployee(this);
        }

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
