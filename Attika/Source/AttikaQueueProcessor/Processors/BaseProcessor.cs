using Infotecs.Attika.AttikaSharedDataObjects.Messages;

namespace Infotecs.Attika.AttikaQueueProcessor.Processors
{
    public sealed class BaseProcessor
    {
        public void Process(BaseMessage message)
        {
            var instance = (dynamic) this;
            instance.Process((dynamic) message);
        }
    }
}