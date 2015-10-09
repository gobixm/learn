using System;

namespace FancyDeliveryApp.Api.Models
{
    public class LineItemDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public Decimal Price { get; set; }
        public ProductDto Product { get; set; }
    }
}