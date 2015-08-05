using System;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.Serialization.Json;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Xml;
using AttikaContracts.Messages;
using FastMember;

namespace Infotecs.Attika.AttikaService.Messages.Wcf.Serializers
{
    public static class JsonMessageSerializer
    {
        public static Message Serialize(Type messageType, BaseMessage message)
        {
            if (WebOperationContext.Current != null)
            {
                var serializer = new DataContractJsonSerializer(messageType);
                return WebOperationContext.Current.CreateJsonResponse(message, serializer);
            }
            return null;
        }

        public static object Deserialize(Message message, Type messageType)
        {
            using (var memoryStream = new MemoryStream())
            {
                XmlDictionaryWriter writer = JsonReaderWriterFactory.CreateJsonWriter(memoryStream);
                message.WriteMessage(writer);
                writer.Flush();
                var serializer = new DataContractJsonSerializer(messageType);
                memoryStream.Seek(0, SeekOrigin.Begin);
                return serializer.ReadObject(memoryStream);
            }
        }

        public static object DeserializeNameValueCollection(Type messageType, NameValueCollection collection)
        {
            ObjectAccessor message = ObjectAccessor.Create(Activator.CreateInstance(messageType));
            foreach (string key in collection.AllKeys)
            {
                try
                {
                    message[key] = collection[key];
                }
// ReSharper disable EmptyGeneralCatchClause
                catch
// ReSharper restore EmptyGeneralCatchClause
                {
                }
            }
            return message.Target;
        }
    }
}