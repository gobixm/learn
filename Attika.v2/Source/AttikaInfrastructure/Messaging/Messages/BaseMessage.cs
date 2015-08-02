using System.Runtime.Serialization;

namespace Infotecs.Attika.AttikaInfrastructure.Messaging.Messages
{
    [DataContract]
    public abstract class BaseMessage
    {
        [DataMember]
        public string Request { get; set; }
    }
}