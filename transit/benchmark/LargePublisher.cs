using System;
using System.Threading;
using System.Threading.Tasks;
using transit.contracts;

namespace transit.benchmark
{
    public class LargePublisher : Publisher
    {
        public override async Task Start(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await bus.Publish(LargeMessage.Generate(correlationId, 10000000), cancellationToken);
            }
        }
    }
}
