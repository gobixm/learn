using System;

namespace Infotecs.Attika.AttikaDomain.Services.Queuing
{
    public interface IQueueService : IDisposable
    {
        void PushMessage(object message);
    }
}