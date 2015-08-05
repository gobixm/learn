using System;
using System.Text;
using Infotecs.Attika.AttikaInfrastructure.Services.Contracts;
using Newtonsoft.Json;

namespace Infotecs.Attika.AttikaInfrastructure.Services
{
    public class MessageSerializationService : IMessageSerializationService
    {
        public byte[] Serialize(object message)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
        }

        public object Deseriallize(byte[] message, Type messageType)
        {
            string decodedMessage = Encoding.UTF8.GetString(message);
            return JsonConvert.DeserializeObject(decodedMessage, messageType);
        }
    }
}