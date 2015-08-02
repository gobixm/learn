using System.ServiceModel.Channels;

namespace Infotecs.Attika.AttikaService.Messages.Processors
{
    public interface IMessageProcessor
    {
        Message HandleMessage(Message message);
    }
}