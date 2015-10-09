using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class LineItem
    {
        public virtual int Id { get; set; }
        public virtual int Quantity { get; set; }
        public virtual Decimal Price { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<Cart> Carts { get; protected set; }
    }
}
