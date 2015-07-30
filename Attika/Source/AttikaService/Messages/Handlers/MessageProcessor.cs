using System;
using System.Collections.Specialized;
using System.Net;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using Infotecs.Attika.AttikaService.Messages.Wcf.Serializers;

namespace Infotecs.Attika.AttikaService.Messages.Handlers
{
    public class MessageProcessor : IMessageProcessor
    {
        public delegate BaseMessage HandleMethodDelegate(BaseMessage message);

        private readonly IConfiguration _configuration;

        public MessageProcessor(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Message HandleMessage(Message message)
        {
            if (WebOperationContext.Current == null)
                return null;

            UriTemplateMatch templateMatch = WebOperationContext.Current.IncomingRequest.UriTemplateMatch;
            NameValueCollection queryParameters = templateMatch.QueryParameters;
            string messageHeader;
            try
            {
                messageHeader = queryParameters["request"];
            }
            catch (Exception)
            {
                throw new WebFaultException(HttpStatusCode.BadRequest);
            }

            BaseHandler handler = _configuration.GetMessageHandler(messageHeader);

            if (handler != null)
            {
                InOutMessageType messageType = _configuration.GetMessageType(messageHeader);
                if (messageType != null)
                {
                    BaseMessage messageObject = null;
                    if (WebOperationContext.Current != null)
                    {
                        NameValueCollection queryParams =
                            WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters;
                        messageObject = (BaseMessage) JsonMessageSerializer.DesearizeNameValueCollection(messageType.In,
                                                                                                         queryParams);
                    }
//                    
//                    {
//                        messageObject = JsonMessageSerializer.Desearize(message, handlerData.MessageType);
//                    }
                    if (messageObject != null)
                    {
                        BaseMessage resultMessage = handler.Handle(messageObject);
                        if (resultMessage != null)
                            return JsonMessageSerializer.Serialize(messageType.Out, resultMessage);
                    }
                }
                return null;
            }
            return null;
        }
    }
}