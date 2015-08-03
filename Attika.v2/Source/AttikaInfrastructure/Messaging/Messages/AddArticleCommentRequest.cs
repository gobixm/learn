using System.Runtime.Serialization;
using Infotecs.Attika.AttikaInfrastructure.Data.DataTransferObjects;

namespace Infotecs.Attika.AttikaInfrastructure.Messaging.Messages
{
    [DataContract]
    public class AddArticleCommentRequest : BaseMessage
    {
        [DataMember]
        public string ArticleId { get; set; }
        [DataMember]
        public CommentDto Comment { get; set; }
    }
}