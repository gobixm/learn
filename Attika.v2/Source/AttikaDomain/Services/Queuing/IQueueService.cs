using System;

namespace Infotecs.Attika.AttikaDomain.Services.Queuing
{
    public interface IQueueService : IDisposable
    {
        void PushMessage(byte[] message);
        void RegisterConsumer(EventHandler<byte[]> arrived);
        void UnregisterConsumer(EventHandler<byte[]> arrived);
    }
}