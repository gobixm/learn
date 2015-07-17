using System;
using System.Collections.Generic;

namespace Infotecs.GangOfFour.Visitor
{
    internal interface IUnit
    {
        string Address { get; set; }
        IList<IUnit> SubUnits { get; set; }
        void Accept(IVisitor visitor);
        void Move(string address);
        void Print(int indent);
    }
}
