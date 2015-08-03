using System.Runtime.Serialization;

namespace Infotecs.Attika.AttikaInfrastructure.Messaging.Messages
{
    [DataContract]
    public class DeleteArticleCommentRequest : BaseMessage
    {
        [DataMember]
        public string ArticleId { get; set; }

        [DataMember]
        public string CommentId { get; set; }
    }
}