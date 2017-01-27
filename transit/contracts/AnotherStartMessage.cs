using System;
using System.Collections.Generic;

namespace transit.contracts
{
    public class AnotherStartMessage
    {
        public Guid CorrelationId { get; set; }
        public List<Guid> Entities { get; set; } = new List<Guid>();
    }
}
