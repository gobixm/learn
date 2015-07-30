namespace Infotecs.Attika.AttikaService.Messages.Handlers
{
    public interface IMessageProcessorConfiguration
    {
        void RegisterHandler(BaseHandler handler);
        BaseHandler GetMessageHandler(string messageHeader);
        InOutMessageType GetMessageType(string messageHeader);
    }
}