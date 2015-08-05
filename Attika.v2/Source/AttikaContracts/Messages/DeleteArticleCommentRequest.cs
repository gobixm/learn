using System.Runtime.Serialization;

namespace AttikaContracts.Messages
{
    [DataContract]
    public class DeleteArticleCommentRequest
    {
        [DataMember]
        public string ArticleId { get; set; }

        [DataMember]
        public string CommentId { get; set; }
    }
}