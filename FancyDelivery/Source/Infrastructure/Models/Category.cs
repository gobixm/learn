using System;
using System.Collections.Generic;

namespace Infrastructure
{
    public class Category
    {
        public Category()
        {
            Products = new List<Product>();
        }

        public virtual string Code { get; set; }
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
