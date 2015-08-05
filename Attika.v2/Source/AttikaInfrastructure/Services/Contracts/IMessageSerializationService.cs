using System;

namespace Infotecs.Attika.AttikaInfrastructure.Services.Contracts
{
    public interface IMessageSerializationService
    {
        byte[] Serialize(object message);
        object Deseriallize(byte[] message, Type messageType);
    }
}