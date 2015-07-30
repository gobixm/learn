using System.ServiceModel.Channels;

namespace Infotecs.Attika.AttikaService.Messages.Handlers
{
    public interface IMessageProcessor
    {
        Message HandleMessage(Message message);
    }
}