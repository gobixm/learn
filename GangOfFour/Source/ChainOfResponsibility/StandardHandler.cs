using System;

namespace Infotecs.GangOfFour.ChainOfResponsibility
{
    internal class StandardHandler<T> : Handler<T>
        where T : IRequest
    {
        public StandardHandler(IHandler successor) : base(successor)
        {
        }

        public override void Handle(IRequest request)
        {
            if (request.GetType() == typeof(T))
            {
                Console.WriteLine("Request {0} was handled by {1}", request.Body, typeof(T).Name);
            }
            else
            {
                Console.WriteLine("Request {0} was not handled by {1}", request.Body, typeof(T).Name);
                if (Successor != null)
                {
                    Successor.Handle(request);
                }
            }
        }
    }
}
