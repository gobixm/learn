using System;
using System.Net;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using Infotecs.Attika.AttikaDomain.Services.Queuing;
using Infotecs.Attika.AttikaInfrastructure.Messaging.Messages;
using Infotecs.Attika.AttikaInfrastructure.Messaging.Serializers;
using Infotecs.Attika.AttikaService.Messages.Wcf.Serializers;

namespace Infotecs.Attika.AttikaService.Messages.Processors
{
    public sealed class MessageProcessor : IMessageProcessor
    {
        public delegate BaseMessage HandleMethodDelegate(BaseMessage message);

        private readonly IMessageProcessorConfiguration _messageProcessorConfiguration;
        private readonly IMessageSerializer _messageSerializer;

        public MessageProcessor(IMessageProcessorConfiguration messageProcessorConfiguration, IQueueService queueService,
            IMessageSerializer messageSerializer)
        {
            _messageProcessorConfiguration = messageProcessorConfiguration;
            _messageSerializer = messageSerializer;
            queueService.RegisterConsumer(HandleMessageFromQueue);
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
                messageHeader = queryParameters["Request"];
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
                    BaseMessage messageObject;

                    if (WebOperationContext.Current.IncomingRequest.Method == "GET")
                    {
                        messageObject =
                            (BaseMessage) JsonMessageSerializer.DeserializeNameValueCollection(messageType.In,
                                queryParameters);
                        messageObject.Request = messageHeader;
                        var resultMessage = handler.Handle(messageObject);
                        if (resultMessage != null)
                        {
                            return JsonMessageSerializer.Serialize(messageType.Out, resultMessage);
                        }
                    }
                    else
                    {
                        messageObject = (BaseMessage) JsonMessageSerializer.Deserialize(message, messageType.In);
                        messageObject.Request = messageHeader;
                        handler.Enqueue(messageObject);
                    }
                }
                return null;
            }
            return null;
        }

        public void HandleMessageFromQueue(byte[] message)
        {
            var decodedMessage = _messageSerializer.Deseriallize(message,
                header => _messageProcessorConfiguration.GetMessageType(header).In);
            var handler = _messageProcessorConfiguration.GetMessageHandler(decodedMessage.Request);
            handler.Handle(decodedMessage);
        }
    }
}