using System;
using System.Threading;
using System.Threading.Tasks;
using Automatonymous;
using GreenPipes;
using MassTransit;
using MassTransit.RabbitMqTransport;
using MassTransit.Saga;
using transit.benchmark;
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

                cfg.UseBsonSerializer();
                cfg.UseConcurrencyLimit(1);

                cfg.ReceiveEndpoint(host, "test_queue", e =>
                {
                    e.PrefetchCount = 8;
                    e.StateMachineSaga(machine, new InMemorySagaRepository<Workflow>());
                    e.StateMachineSaga(anotherMachine, new InMemorySagaRepository<Workflow>());
                });

                cfg.ReceiveEndpoint(host, "benchmark_queue", e =>
                {
                    e.PrefetchCount = 1;
                    e.Consumer<SimpleConsumer>();
                });
            });
        }

        private static void Main(string[] args)
        {
            IBusControl busControl = ConfigureBus();

            //            var emitter = new Emitter();
            //            emitter.Emit();
            //            emitter.EmitError();
            //            emitter.EmitAnother();
            //            emitter.EmitError();
            //            emitter.EmitAnother();

            var smallPublisher = new SmallPublisher();
            var moderatePublisher = new ModeratePublisher();
            var largePublisher = new LargePublisher();

            var tokenSource = new CancellationTokenSource();
            tokenSource.CancelAfter(10000);

            busControl.Start();
            try
            {
                Task.WaitAll(new[]
            {
                smallPublisher.Start(tokenSource.Token),
                moderatePublisher.Start(tokenSource.Token),
                largePublisher.Start(tokenSource.Token)
            }, tokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("done");
            }

            busControl.Stop();
            Counter.Stop();
        }
    }
}
