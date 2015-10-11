using System;

namespace Infrastructure
{
    public class Product
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual Category Category { get; set; }
        public virtual string ImageName { get; set; }
        public virtual Decimal Price { get; set; }
    }
}