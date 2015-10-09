using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FancyDeliveryApp.Api.Models
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public List<LineItemDto> LineItems {get; set; }
    }
}