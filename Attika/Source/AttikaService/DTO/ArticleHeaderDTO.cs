using System;
using System.Runtime.Serialization;

namespace Infotecs.Attika.AttikaService.DTO
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