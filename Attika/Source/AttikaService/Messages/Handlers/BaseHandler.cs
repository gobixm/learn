namespace Infotecs.Attika.AttikaService.Messages.Handlers
{
    public class BaseHandler
    {
        public BaseMessage Handle(BaseMessage message)
        {
            dynamic handler = this;
            return (BaseMessage) handler.Handle(message);
        }
    }
}