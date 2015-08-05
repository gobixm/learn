using System.Runtime.Serialization;

namespace AttikaContracts.Messages
{
    [DataContract]
    public class GetArticleRequest
    {
        [DataMember]
        public string Id { get; set; }
    }
}