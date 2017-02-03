using System;
using System.Threading.Tasks;
using MassTransit;
using transit.contracts;

namespace transit.benchmark
{
    public class SimpleConsumer : IConsumer<SmallMessage>, IConsumer<ModerateMessage>, IConsumer<LargeMessage>
    {
        public async Task Consume(ConsumeContext<SmallMessage> context)
        {
            await Task.Run(() => Counter.CountSmall());
        }

        public async Task Consume(ConsumeContext<ModerateMessage> context)
        {
            await Task.Run(() => Counter.CountModerate());
        }

        public async Task Consume(ConsumeContext<LargeMessage> context)
        {
            await Task.Run(() => Counter.CountLarge());
        }
    }
}
