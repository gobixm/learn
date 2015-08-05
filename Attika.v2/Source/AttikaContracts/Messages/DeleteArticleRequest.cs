using System.Runtime.Serialization;

namespace AttikaContracts.Messages
{
    [DataContract]
    public class DeleteArticleRequest
    {
        [DataMember]
        public string ArticleId { get; set; }
    }
}