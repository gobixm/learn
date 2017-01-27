using System;
using Automatonymous;
using MassTransit;
using MassTransit.RabbitMqTransport;
using MassTransit.Saga;
using transit.workflows;

namespace transit
{
    internal class Program
    {
        private static readonly AnotherWorkflowStateMachine anotherMachine = new AnotherWorkflowStateMachine();
        private static readonly WorkflowStateMachine machine = new WorkflowStateMachine();

        private static IBusControl ConfigureBus()
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                IRabbitMqHost host = cfg.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint(host, "test_queue", e =>
                {
                    e.PrefetchCount = 8;
                    e.StateMachineSaga(machine, new InMemorySagaRepository<Workflow>());
                    e.StateMachineSaga(anotherMachine, new InMemorySagaRepository<Workflow>());
                });
            });
        }

        private static void Main(string[] args)
        {
            IBusControl busControl = ConfigureBus();
            busControl.Start();

            var emitter = new Emitter();
            emitter.Emit();
            emitter.EmitError();
            emitter.EmitAnother();
            emitter.EmitError();
            emitter.EmitAnother();

            do
            {
            }
            while (true);

            busControl.Stop();
        }
    }
}
