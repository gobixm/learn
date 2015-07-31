using System.Runtime.Serialization;

namespace Infotecs.Attika.AttikaSharedDataObjects.Messages
{
    [DataContract]
    public abstract class BaseMessage
    {
        [DataMember]
        public string Request { get; set; }
    }
}