using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeTraveller
{
    public class Index
    {
        public string Symbol { get; set; }
        public DateTime Date { get; set; }
        public Decimal Open { get; set; }
        public Decimal High { get; set; }
        public Decimal Low { get; set; }
        public Decimal Close { get; set; }
        public long Volume { get; set; }
        public Decimal AdjClose { get; set; }
    }
}
