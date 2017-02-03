using System;
using System.Threading;
using System.Threading.Tasks;
using transit.contracts;

namespace transit.benchmark
{
    public class ModeratePublisher : Publisher
    {
        public override async Task Start(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await bus.Publish(ModerateMessage.Generate(correlationId, 100000), cancellationToken);
            }
        }
    }
}
