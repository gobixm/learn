using System;

namespace Infotecs.GangOfFour.Mediator
{
    internal interface IWorker
    {
        IMediator Mediator { get; }
        void PromoteToRiot();
        void Riot();
    }
}
