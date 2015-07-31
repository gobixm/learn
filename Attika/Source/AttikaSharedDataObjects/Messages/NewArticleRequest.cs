using System.Runtime.Serialization;
using Infotecs.Attika.AttikaSharedDataObjects.DataTransferObjects;

namespace Infotecs.Attika.AttikaSharedDataObjects.Messages
{
    [DataContract]
    public class NewArticleRequest : BaseMessage
    {
        [DataMember]
        public ArticleDto Article { get; set; }
    }
}