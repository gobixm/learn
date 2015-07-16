using System;

namespace Infotecs.GangOfFour.ChainOfResponsibility
{
    internal abstract class Handler<T> : IHandler<T>
        where T : IRequest
    {
        protected Handler(IHandler successor)
        {
            Successor = successor;
        }

        protected IHandler Successor { get; set; }

        public abstract void Handle(IRequest request);

        void IHandler<T>.Handle(T request)
        {
            Handle(request);
        }
    }
}
