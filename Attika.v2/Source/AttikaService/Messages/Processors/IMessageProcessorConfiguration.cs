using Infotecs.Attika.AttikaDomain.Services.RequestProcessors;

namespace Infotecs.Attika.AttikaService.Messages.Processors
{
    public interface IMessageProcessorConfiguration
    {
        void RegisterHandler(BaseHandler handler);
        BaseHandler GetMessageHandler(string messageHeader);
        InOutMessageType GetMessageType(string messageHeader);
    }
}