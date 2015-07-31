using System;
using System.Collections.Specialized;
using System.Net;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using Infotecs.Attika.AttikaService.Messages.Wcf.Serializers;
using Infotecs.Attika.AttikaSharedDataObjects.Messages;

namespace Infotecs.Attika.AttikaService.Messages.Handlers
{
    public sealed class MessageProcessor : IMessageProcessor
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

            UriTemplateMatch templateMatch = WebOperationContext.Current.IncomingRequest.UriTemplateMatch;
            NameValueCollection queryParameters = templateMatch.QueryParameters;
            string messageHeader;
            try
            {
                messageHeader = queryParameters["Request"];
            }
            catch (Exception)
            {
                throw new WebFaultException(HttpStatusCode.BadRequest);
            }

            BaseHandler handler = _messageProcessorConfiguration.GetMessageHandler(messageHeader);

            if (handler != null)
            {
                InOutMessageType messageType = _messageProcessorConfiguration.GetMessageType(messageHeader);
                if (messageType != null)
                {
                    BaseMessage messageObject;

                    if (WebOperationContext.Current.IncomingRequest.Method == "GET")
                    {
                        messageObject =
                            (BaseMessage) JsonMessageSerializer.DeserializeNameValueCollection(messageType.In,
                                                                                               queryParameters);
                    }
                    else
                    {
                        messageObject = (BaseMessage) JsonMessageSerializer.Deserialize(message, messageType.In);
                    }
                    if (messageObject != null)
                    {
                        messageObject.Request = messageHeader;
                        BaseMessage resultMessage = handler.Handle(messageObject);
                        if (resultMessage != null)
                        {
                            return JsonMessageSerializer.Serialize(messageType.Out, resultMessage);
                        }
                    }
                }
                return null;
            }
            return null;
        }
    }
}