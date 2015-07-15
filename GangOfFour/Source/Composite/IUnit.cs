using System;
using System.Collections.Generic;

namespace Infotecs.GangOfFour.Composite
{
    internal interface IUnit
    {
        string Address { get; set; }
        IList<IUnit> SubUnits { get; set; }
        void Move(string address);
        void Print(int indent);
    }
}
