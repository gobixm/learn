using System;

namespace transit.contracts
{
    public class ProgressMessage
    {
        public Guid CorrelationId { get; set; }
        public Guid Entity { get; set; }

        public bool Fail { get; set; }
    }
}
