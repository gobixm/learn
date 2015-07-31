using System.Collections.Generic;
using System.Runtime.Serialization;
using Infotecs.Attika.AttikaSharedDataObjects.DataTransferObjects;

namespace Infotecs.Attika.AttikaSharedDataObjects.Messages
{
    [DataContract]
    public sealed class GetArticleHeadersResponse : BaseMessage
    {
        [DataMember]
        public List<ArticleHeaderDto> Headers { get; set; }
    }
}