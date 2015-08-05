using System.Runtime.Serialization;
using AttikaContracts.DataTransferObjects;

namespace AttikaContracts.Messages
{
    [DataContract]
    public class GetArticleResponse
    {
        [DataMember]
        public ArticleDto Article { get; set; }
    }
}