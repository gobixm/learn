using System;

namespace Infotecs.Attika.AttikaInfrastructure.Services.Contracts
{
    public interface IMessageSerializationService
    {
        object Deseriallize(byte[] message, Type messageType);
        byte[] Serialize(object message);
    }
}
