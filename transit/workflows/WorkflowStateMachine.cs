using System;
using Automatonymous;
using transit.contracts;

namespace transit.workflows
{
    public class WorkflowStateMachine : MassTransitStateMachine<Workflow>
    {
        public WorkflowStateMachine()
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

        private State Active { get; set; }
        private Event<ProgressMessage> Progress { get; set; }
        private Event<StartMessage> Started { get; set; }

        private static void HandleProgress(BehaviorContext<Workflow, ProgressMessage> x)
        {
            Console.WriteLine($"progress:{x.Data.CorrelationId}-fail:{x.Data.Fail}");
            x.Instance.FullfilledEntities.Add(x.Data.Entity);
        }

        private void HandleProgressError(BehaviorContext<Workflow, ProgressMessage> x)
        {
            Console.WriteLine($"progress:{x.Data.CorrelationId}-fail:{x.Data.Fail}");
        }

        private void HandleStarted(BehaviorContext<Workflow, StartMessage> obj)
        {
            Console.WriteLine($"started {obj.Data.CorrelationId}");
        }

        private void NotifyError(BehaviorContext<Workflow, ProgressMessage> obj)
        {
            Console.WriteLine("error");
        }

        private void NotifySuccess(BehaviorContext<Workflow, ProgressMessage> obj)
        {
            Console.WriteLine("success");
        }
    }
}
