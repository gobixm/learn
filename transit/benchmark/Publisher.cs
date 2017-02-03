using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;

namespace transit.benchmark
{
    public abstract class Publisher
    {
        protected readonly IBusControl bus;
        protected readonly Guid correlationId;

        protected Publisher()
        {
            bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });

            correlationId = Guid.NewGuid();
        }

        public abstract Task Start(CancellationToken cancellationToken);
    }
}
