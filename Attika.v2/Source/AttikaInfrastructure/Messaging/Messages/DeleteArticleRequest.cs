using System.Runtime.Serialization;

namespace Infotecs.Attika.AttikaInfrastructure.Messaging.Messages
{
    [DataContract]
    public class DeleteArticleRequest : BaseMessage
    {
        [DataMember]
        public string ArticleId { get; set; }
    }
}