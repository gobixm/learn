using System;
using System.Collections.Generic;
using System.Linq;

namespace transit.contracts
{
    public class SmallMessage
    {
//        public Dictionary<int, Payload> Collection { get; set; }
        public Guid CorrelationId { get; set; }
//        public double Double { get; set; }
//        public long Long { get; set; }
//        public ulong ULong { get; set; }

        public static SmallMessage Generate(Guid correlationId)
        {
            Dictionary<int, Payload> collection = Enumerable.Range(0, 10)
                .ToDictionary(x => x, x => new Payload { Field = x.ToString() });

            var random = new Random();
            return new SmallMessage
            {
                CorrelationId = correlationId,
                //Long = (long)random.Next() << 32 + random.Next(),
                //Collection = collection,
                //Double = random.NextDouble(),
                //ULong = (ulong)random.Next() << 32 + random.Next(),
            };
        }

        public class Payload
        {
            public string Field { get; set; }
        }
    }
}
