using System;
using System.Runtime.Serialization;

namespace AttikaContracts.DataTransferObjects
{
    [DataContract]
    public class ArticleHeaderDto
    {
        [DataMember]
        public Guid ArticleId { get; set; }

        [DataMember]
        public string Title { get; set; }
    }
}