using System;

namespace Infotecs.Attika.AttikaService.Messages.Handlers
{
    public class BaseHandler : ICloneable
    {
        public virtual object Clone()
        {
            return new BaseHandler();
        }

        public BaseMessage Handle(BaseMessage message)
        {
            dynamic handler = this;
            return (BaseMessage) handler.Handle(message);
        }
    }
}