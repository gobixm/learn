using System;
using System.ServiceModel.Channels;

namespace Infotecs.Attika.AttikaService.Messages.Processors
{
    public interface IMessageProcessor : IDisposable
    {
        Message HandleMessage(Message message);
    }
}