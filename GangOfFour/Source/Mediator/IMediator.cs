using System;

namespace Infotecs.GangOfFour.Mediator
{
    internal interface IMediator
    {
        void AttachWorker(IWorker worker);
        void RiotRequest(IWorker sender);
    }
}
