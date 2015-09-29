using System;

namespace FancyDeliveryApp.Api.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public Decimal Price { get; set; }
    }
}
