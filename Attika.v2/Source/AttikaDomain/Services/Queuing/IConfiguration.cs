using System;

namespace Infotecs.Attika.AttikaDomain.Services.Queuing
{
    public interface IConfiguration
    {
        void Bind<TMessage, TProcessor>(Func<TProcessor> creator) where TMessage : class
            where TProcessor : IQueueProcessor;
    }
}
