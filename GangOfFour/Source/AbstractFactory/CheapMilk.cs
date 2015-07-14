using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infotecs.GangOfFour.AbstractFactory
{
    internal sealed class CheapMilk : IProduct
    {
        public string Name
        {
            get { return this.GetType().Name; }
        }
    }
}
