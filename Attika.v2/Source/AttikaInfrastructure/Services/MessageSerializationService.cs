using System;
using System.IO;
using System.Text;
using Infotecs.Attika.AttikaInfrastructure.Messaging.Messages;
using Infotecs.Attika.AttikaInfrastructure.Services.Contracts;
using Newtonsoft.Json;

namespace Infotecs.Attika.AttikaInfrastructure.Services
{
    public class MessageSerializationService : IMessageSerializationService
    {
        public byte[] Serialize(BaseMessage message)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
        }

        public BaseMessage Deseriallize(byte[] message, Func<string, Type> messageTypeByNameFunc)
        {
            var decodedMessage = Encoding.UTF8.GetString(message);
            JsonReader jsonReader = new JsonTextReader(new StringReader(decodedMessage));
            var messageHeader = "";
            while (jsonReader.Read())
            {
                if ((jsonReader.TokenType == JsonToken.PropertyName) && (jsonReader.Value.ToString() == "Request"))
                {
                    jsonReader.Read();
                    messageHeader = jsonReader.Value.ToString();
                    break;
                }
            }
            if (messageHeader.Length > 0)
            {
                return (BaseMessage) JsonConvert.DeserializeObject(decodedMessage, messageTypeByNameFunc(messageHeader));
            }
            return null;
        }
    }
}