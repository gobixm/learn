using System;

namespace Infotecs.GangOfFour.ChainOfResponsibility
{
    internal interface IHandler
    {
        void Handle(IRequest request);
    }

    internal interface IHandler<in T> : IHandler
        where T : IRequest
    {
        void Handle(T request);
    }
}
