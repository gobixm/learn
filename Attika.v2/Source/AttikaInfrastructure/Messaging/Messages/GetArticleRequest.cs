using System.Runtime.Serialization;

namespace Infotecs.Attika.AttikaInfrastructure.Messaging.Messages
{
    [DataContract]
    public class GetArticleRequest : BaseMessage
    {
        [DataMember]
        public string Id { get; set; }
    }
}