using System;
using System.Collections.Generic;

namespace Infotecs.GangOfFour.Visitor
{
    internal class Group : IUnit
    {
        public Group()
        {
            SubUnits = new List<IUnit>();
            Address = "";
        }

        public string Address { get; set; }
        public IList<IUnit> SubUnits { get; set; }

        public void Accept(IVisitor visitor)
        {
            visitor.VisitGroup(this);
            foreach (IUnit unit in SubUnits)
            {
                unit.Accept(visitor);
            }
        }

        public void Move(string address)
        {
            Address = address;
            foreach (IUnit unit in SubUnits)
            {
                unit.Move(address);
            }
        }

        public void Print(int indent)
        {
            Console.WriteLine("".PadLeft(indent) + GetType().Name + "->" + Address);
            foreach (IUnit unit in SubUnits)
            {
                unit.Print(indent + 1);
            }
        }
    }
}
