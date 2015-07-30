using System;
using System.Net;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using Infotecs.Attika.AttikaService.Messages.Wcf.Serializers;

namespace Infotecs.Attika.AttikaService.Messages.Handlers
{
    public class MessageProcessor : IMessageProcessor
    {
        public delegate BaseMessage HandleMethodDelegate(BaseMessage message);

        private readonly IMessageProcessorConfiguration _messageProcessorConfiguration;

        public MessageProcessor(IMessageProcessorConfiguration messageProcessorConfiguration)
        {
            _messageProcessorConfiguration = messageProcessorConfiguration;
        }

        public Message HandleMessage(Message message)
        {
            if (WebOperationContext.Current == null)
                return null;

            var templateMatch = WebOperationContext.Current.IncomingRequest.UriTemplateMatch;
            var queryParameters = templateMatch.QueryParameters;
            string messageHeader;
            try
            {
                messageHeader = queryParameters["request"];
            }
            catch (Exception)
            {
                throw new WebFaultException(HttpStatusCode.BadRequest);
            }

            var handler = _messageProcessorConfiguration.GetMessageHandler(messageHeader);

            if (handler != null)
            {
                var messageType = _messageProcessorConfiguration.GetMessageType(messageHeader);
                if (messageType != null)
                {
                    BaseMessage messageObject = null;
                    if (WebOperationContext.Current != null)
                    {
                        var queryParams =
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
                        var resultMessage = handler.Handle(messageObject);
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