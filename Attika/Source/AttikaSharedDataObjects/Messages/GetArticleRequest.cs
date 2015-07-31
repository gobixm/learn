using System.Runtime.Serialization;

namespace Infotecs.Attika.AttikaSharedDataObjects.Messages
{
    [DataContract]
    public class GetArticleRequest : BaseMessage
    {
        [DataMember]
        public string Id { get; set; }
    }
}