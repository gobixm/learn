using Infotecs.Attika.AttikaSharedDataObjects.Messages;

namespace Infotecs.Attika.AttikaService.Messages.Queues
{
    public interface IQueuePusher
    {
        void PushMessage(BaseMessage message);
    }
}