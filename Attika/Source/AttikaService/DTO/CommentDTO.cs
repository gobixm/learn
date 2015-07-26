using System;
using System.Runtime.Serialization;

namespace Infotecs.Attika.AttikaService.DTO
{
    [DataContract]
    public sealed class CommentDto
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public DateTime Created { get; set; }
    }
}