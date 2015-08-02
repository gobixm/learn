using System.Collections.Generic;
using System.Runtime.Serialization;
using Infotecs.Attika.AttikaInfrastructure.Data.DataTransferObjects;

namespace Infotecs.Attika.AttikaInfrastructure.Messaging.Messages
{
    [DataContract]
    public sealed class GetArticleHeadersResponse : BaseMessage
    {
        [DataMember]
        public List<ArticleHeaderDto> Headers { get; set; }
    }
}