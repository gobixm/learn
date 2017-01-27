using System;
using System.Collections.Generic;
using System.Linq;
using MassTransit;
using transit.contracts;

namespace transit
{
    public class Emitter
    {
        public async void Emit()
        {
            IBusControl busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });

            Guid correlation = Guid.NewGuid();
            List<Guid> entities = Enumerable.Range(1, 10)
                .Select(x => Guid.NewGuid())
                .ToList();

            await busControl.Publish(new StartMessage
            {
                CorrelationId = correlation,
                Entities = entities
            });

            foreach (Guid entity in entities)
            {
                await busControl.Publish(new ProgressMessage
                {
                    CorrelationId = correlation,
                    Entity = entity
                });
            }
        }

        public async void EmitAnother()
        {
            IBusControl busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });

            Guid correlation = Guid.NewGuid();
            List<Guid> entities = Enumerable.Range(1, 10)
                .Select(x => Guid.NewGuid())
                .ToList();

            await busControl.Publish(new AnotherStartMessage
            {
                CorrelationId = correlation,
                Entities = entities
            });

            foreach (Guid entity in entities)
            {
                await busControl.Publish(new ProgressMessage
                {
                    CorrelationId = correlation,
                    Entity = entity
                });
            }
        }

        public async void EmitError()
        {
            IBusControl busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });

            Guid correlation = Guid.NewGuid();
            List<Guid> entities = Enumerable.Range(1, 10)
                .Select(x => Guid.NewGuid())
                .ToList();

            await busControl.Publish(new StartMessage
            {
                CorrelationId = correlation,
                Entities = entities
            });

            foreach (Guid entity in entities)
            {
                await busControl.Publish(new ProgressMessage
                {
                    CorrelationId = correlation,
                    Entity = entity,
                    Fail = true
                });
            }
        }
    }
}
