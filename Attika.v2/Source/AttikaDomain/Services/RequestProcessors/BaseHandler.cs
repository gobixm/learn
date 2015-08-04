using System;
using Infotecs.Attika.AttikaDomain.Services.Exceptions;
using Infotecs.Attika.AttikaDomain.Services.Metadata;
using Infotecs.Attika.AttikaInfrastructure.Messaging.Messages;
using Microsoft.CSharp.RuntimeBinder;

namespace Infotecs.Attika.AttikaDomain.Services.RequestProcessors
{
    public class BaseHandler : ICloneable
    {
        public virtual object Clone()
        {
            return new BaseHandler();
        }

        public BaseMessage Handle(BaseMessage message)
        {
            try
            {
                //BaseMessage is abstract, so no recursion problems here
                return (BaseMessage) ((dynamic) this).Handle((dynamic) message);
            }
            catch (RuntimeBinderException ex)
            {
                throw new ServiceException(ServiceMetadata.UnsupportedMethod, ex);
            }
        }

        public void Enqueue(BaseMessage message)
        {
            try
            {
                ((dynamic) this).Enqueue((dynamic) message);
            }
            catch (RuntimeBinderException ex)
            {
                throw new ServiceException(ServiceMetadata.UnsupportedMethod, ex);
            }
        }
    }
}