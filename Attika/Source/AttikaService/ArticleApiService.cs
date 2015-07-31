using System.ServiceModel;
using System.ServiceModel.Channels;
using Infotecs.Attika.AttikaService.Messages.Handlers;

namespace Infotecs.Attika.AttikaService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public sealed class ArticleApiService : IArticleApiService
    {
        private readonly IMessageProcessor _messageProcessor;

        public ArticleApiService(IMessageProcessor messageProcessor)
        {
            _messageProcessor = messageProcessor;
        }

        public Message Get(Message message)
        {
            return _messageProcessor.HandleMessage(message);
        }

        public void Post(Message message)
        {
            _messageProcessor.HandleMessage(message);
        }

        public void Delete(Message message)
        {
            _messageProcessor.HandleMessage(message);
        }
    }
}