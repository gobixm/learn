using System;
using System.Linq;

namespace transit.contracts
{
    public class LargeMessage
    {
        public Guid CorrelationId { get; set; }

        public byte[] Blob { get; set; }

        public static LargeMessage Generate(Guid correlationId, int size)
        {
            Random random = new Random();
            byte[] blob = new byte[size];
            random.NextBytes(blob);
            return new LargeMessage
            {
                CorrelationId = correlationId,
                Blob = blob
            };
        }
    }
}
