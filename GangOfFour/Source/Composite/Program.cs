using System;
using System.Collections.Generic;

namespace Infotecs.GangOfFour.Composite
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IUnit root = new Group();
            root.SubUnits.Add(
                new Group
                {
                    SubUnits = new List<IUnit>
                    {
                        new Employee(),
                        new Employee(),
                        new Employee()
                    }
                }
                );
            root.SubUnits.Add(
                new Group
                {
                    SubUnits = new List<IUnit>
                    {
                        new Group
                        {
                            SubUnits = new List<IUnit>
                            {
                                new Employee(),
                                new Employee(),
                                new Employee()
                            }
                        },
                        new Employee()
                    }
                }
                );

            root.Print(0);
            root.SubUnits[1].Move("elmstreet 666");
            Console.WriteLine("moved to hell");
            root.Print(0);

            Console.ReadKey();
        }
    }
}
