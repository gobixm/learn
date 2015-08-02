namespace Infotecs.Attika.AttikaService.Messages.Handlers
{
    public interface IProcessorConfiguration
    {
        void RegisterHandler(BaseHandler handler);
        BaseHandler GetMessageHandler(string messageHeader);
        InOutMessageType GetMessageType(string messageHeader);
    }
}