using System;
using System.Collections.Generic;
using System.Linq;
using Automatonymous;

namespace transit.workflows
{
    public class Workflow : SagaStateMachineInstance
    {
        public bool AllDone => AllEntities.All(x => FullfilledEntities.Contains(x));
        public List<Guid> AllEntities { get; set; } = new List<Guid>();
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }
        public List<Guid> FullfilledEntities { get; set; } = new List<Guid>();
    }
}
