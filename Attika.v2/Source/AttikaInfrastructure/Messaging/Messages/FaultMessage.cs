using System.Runtime.Serialization;

namespace Infotecs.Attika.AttikaInfrastructure.Messaging.Messages
{
    [DataContract]
    public class FaultMessage : BaseMessage
    {
        public FaultMessage(string message, string detail)
        {
            Message = message;
            Detail = detail;
        }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string Detail { get; set; }
    }
}