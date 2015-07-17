using System;

namespace Infotecs.GangOfFour.Visitor
{
    internal sealed class AddressValidationVisitor : IVisitor
    {
        public void VisitEmployee(Employee employee)
        {
            if (employee.Address.Trim().Length == 0)
            {
                Console.WriteLine("Employee visited, address is invalid");
            }
            else
            {
                Console.WriteLine("Employee visited, address is valid");
            }
        }

        public void VisitGroup(Group @group)
        {
            if (@group.Address.Trim().Length == 0)
            {
                Console.WriteLine("Group visited, address is invalid");
            }
            else
            {
                Console.WriteLine("Group visited, address is valid");
            }
        }
    }
}
