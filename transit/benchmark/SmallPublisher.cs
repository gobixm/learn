using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using transit.contracts;

namespace transit.benchmark
{
    public class SmallPublisher : Publisher
    {
        public override async Task Start(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                 await bus.Publish(SmallMessage.Generate(correlationId), cancellationToken);
            }
        }
    }
}
