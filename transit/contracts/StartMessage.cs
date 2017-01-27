using System;
using System.Collections.Generic;

namespace transit.contracts
{
    public class StartMessage
    {
        public Guid CorrelationId { get; set; }
        public List<Guid> Entities { get; set; } = new List<Guid>();
    }
}
