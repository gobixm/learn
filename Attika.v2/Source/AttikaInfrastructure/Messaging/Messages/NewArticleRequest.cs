using System.Runtime.Serialization;
using Infotecs.Attika.AttikaInfrastructure.Data.DataTransferObjects;

namespace Infotecs.Attika.AttikaInfrastructure.Messaging.Messages
{
    [DataContract]
    public class NewArticleRequest : BaseMessage
    {
        [DataMember]
        public ArticleDto Article { get; set; }
    }
}