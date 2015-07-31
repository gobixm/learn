using System;
using Infotecs.Attika.AttikaSharedDataObjects.Messages;

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
            //BaseMessage is abstract, so no recursion problems here
            return (BaseMessage) ((dynamic) this).Handle((dynamic) message);
        }
    }
}