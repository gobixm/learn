using System;

namespace Infotecs.GangOfFour.Visitor
{
    internal interface IVisitor
    {
        void VisitEmployee(Employee employee);
        void VisitGroup(Group group);
    }
}
