using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infotecs.GangOfFour.AbstractFactory
{
    internal abstract class ProductFactory
    {
        public abstract IProduct CreateBread();
        public abstract IProduct CreateMilk();
    }
}
