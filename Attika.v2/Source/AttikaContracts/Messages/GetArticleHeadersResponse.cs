using System.Collections.Generic;
using System.Runtime.Serialization;
using AttikaContracts.DataTransferObjects;

namespace AttikaContracts.Messages
{
    [DataContract]
    public sealed class GetArticleHeadersResponse
    {
        [DataMember]
        public List<ArticleHeaderDto> Headers { get; set; }
    }
}