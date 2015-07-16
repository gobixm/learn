using System;

namespace Infotecs.GangOfFour.Mediator
{
    internal class SimpleWorker : IWorker
    {
        public SimpleWorker(IMediator mediator)
        {
            Mediator = mediator;
            mediator.AttachWorker(this);
        }

        public IMediator Mediator { get; private set; }

        public void PromoteToRiot()
        {
            Mediator.RiotRequest(this);
        }

        public void Riot()
        {
            Console.WriteLine(GetType().Name + " is rioting!");
        }
    }
}
