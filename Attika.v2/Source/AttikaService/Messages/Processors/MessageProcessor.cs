using System;
using System.Collections.Specialized;
using System.Net;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using Infotecs.Attika.AttikaDomain.Services.Exceptions;
using Infotecs.Attika.AttikaDomain.Services.Queuing;
using Infotecs.Attika.AttikaDomain.Services.RequestProcessors;
using Infotecs.Attika.AttikaInfrastructure.Data.DataTransferObjects;
using Infotecs.Attika.AttikaInfrastructure.Messaging.Messages;
using Infotecs.Attika.AttikaInfrastructure.Messaging.Serializers;
using Infotecs.Attika.AttikaService.Messages.Wcf.Serializers;

namespace Infotecs.Attika.AttikaService.Messages.Processors
{
    public sealed class MessageProcessor : IMessageProcessor
    {
        public delegate BaseMessage HandleMethodDelegate(BaseMessage message);

        private readonly IMessageProcessorConfiguration _messageProcessorConfiguration;
        private readonly IMessageSerializationService _messageSerializationService;

        public MessageProcessor(IMessageProcessorConfiguration messageProcessorConfiguration, IQueueService queueService,
                                IMessageSerializationService messageSerializationService)
        {
            _messageProcessorConfiguration = messageProcessorConfiguration;
            _messageSerializationService = messageSerializationService;
            queueService.RegisterConsumer(HandleMessageFromQueue);
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
                        messageObject.Request = messageHeader;
                        try
                        {
                            BaseMessage resultMessage = handler.Handle(messageObject);
                            if (resultMessage != null)
                            {
                                return JsonMessageSerializer.Serialize(messageType.Out, resultMessage);
                            }
                        }
                        catch (ServiceException ex)
                        {
                            throw new WebFaultException<WebFaultDto>(new WebFaultDto(ex.Message, ex.ToString()),
                                                                     HttpStatusCode.Forbidden);
                        }
                        catch (Exception ex)
                        {
                            throw new WebFaultException<WebFaultDto>(
                                new WebFaultDto("Неизвестная ошибка на сервере", ex.ToString()),
                                HttpStatusCode.Forbidden);
                        }
                    }
                    else
                    {
                        messageObject = (BaseMessage) JsonMessageSerializer.Deserialize(message, messageType.In);
                        messageObject.Request = messageHeader;
                        try
                        {
                            handler.Enqueue(messageObject);
                        }
                        catch (ServiceException ex)
                        {
                            throw new WebFaultException<WebFaultDto>(new WebFaultDto(ex.Message, ex.ToString()),
                                                                     HttpStatusCode.Forbidden);
                        }
                        catch (Exception ex)
                        {
                            throw new WebFaultException<WebFaultDto>(
                                new WebFaultDto("Неизвестная ошибка на сервере", ex.ToString()),
                                HttpStatusCode.Forbidden);
                        }
                    }
                }
                return null;
            }
            return null;
        }

        private void HandleMessageFromQueue(byte[] message)
        {
            BaseMessage decodedMessage = _messageSerializationService.Deseriallize(message,
                                                                                   header =>
                                                                                   _messageProcessorConfiguration
                                                                                       .GetMessageType(header).In);
            BaseHandler handler = _messageProcessorConfiguration.GetMessageHandler(decodedMessage.Request);
            handler.Handle(decodedMessage);
        }
    }
}