using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class Cart
    {
        public Cart()
        {
            LineItems = new List<LineItem>();
        }
        public virtual Guid Id { get; set; }
        public virtual ICollection<LineItem> LineItems { get; set; }
    }
}
