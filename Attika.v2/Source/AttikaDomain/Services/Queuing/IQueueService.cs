using System;

namespace Infotecs.Attika.AttikaDomain.Services.Queuing
{
    public interface IQueueService
    {
        void PushMessage(byte[] message);
        void RegisterConsumer(Action<byte[]> arrived);
    }
}