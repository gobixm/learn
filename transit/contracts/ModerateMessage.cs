using System;

namespace transit.contracts
{
    public class ModerateMessage
    {
        public Guid CorrelationId { get; set; }

        public string Blob { get; set; }

        public static ModerateMessage Generate(Guid correlationId, int size)
        {
            return new ModerateMessage
            {
                CorrelationId = correlationId,
                Blob = new string('x', size)
            };
        }
    }
}