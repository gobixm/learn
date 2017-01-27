using System;
using Automatonymous;
using transit.contracts;

namespace transit.workflows
{
    public class AnotherWorkflowStateMachine : MassTransitStateMachine<Workflow>
    {
        public AnotherWorkflowStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => Started, x => x.CorrelateById(y => y.Message.CorrelationId));
            Event(() => Progress, x => x.CorrelateById(y => y.Message.CorrelationId));

            Initially(
                When(Started)
                    .Then(HandleStarted)
                    .Then(x => x.Instance.AllEntities = x.Data.Entities)
                    .TransitionTo(Active));

            During(Active,
                When(Progress, x => x.Data.Fail == false)
                    .Then(HandleProgress),
                When(Progress, x => x.Data.Fail)
                    .Then(HandleProgressError)
                    .Then(NotifyError)
                    .Finalize(),
                When(Progress, x => x.Instance.AllDone)
                    .Then(NotifySuccess)
                    .Finalize());

            SetCompletedWhenFinalized();
        }

        private void HandleStarted(BehaviorContext<Workflow, AnotherStartMessage> obj)
        {
            Console.WriteLine($"another started {obj.Data.CorrelationId}");
        }

        public State Active { get; private set; }
        public Event<ProgressMessage> Progress { get; private set; }
        public Event<AnotherStartMessage> Started { get; private set; }

        private static void HandleProgress(BehaviorContext<Workflow, ProgressMessage> x)
        {
            Console.WriteLine($"another progress:{x.Data.CorrelationId}-fail:{x.Data.Fail}");
            x.Instance.FullfilledEntities.Add(x.Data.Entity);
        }

        private void HandleProgressError(BehaviorContext<Workflow, ProgressMessage> x)
        {
            Console.WriteLine($"another progress:{x.Data.CorrelationId}-fail:{x.Data.Fail}");
        }

        private void NotifyError(BehaviorContext<Workflow, ProgressMessage> obj)
        {
            Console.WriteLine("another error");
        }

        private void NotifySuccess(BehaviorContext<Workflow, ProgressMessage> obj)
        {
            Console.WriteLine("another success");
        }
    }
}
