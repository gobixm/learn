using System.Runtime.Serialization;
using AttikaContracts.DataTransferObjects;

namespace AttikaContracts.Messages
{
    [DataContract]
    public class AddArticleCommentRequest
    {
        [DataMember]
        public string ArticleId { get; set; }

        [DataMember]
        public CommentDto Comment { get; set; }
    }
}