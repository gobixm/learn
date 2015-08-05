using System;
using System.Runtime.Serialization;

namespace AttikaContracts.DataTransferObjects
{
    [DataContract]
    public sealed class CommentDto
    {
        public CommentDto()
        {
            Text = "";
        }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public DateTime Created { get; set; }

        [DataMember]
        public Guid ArticleId { get; set; }
    }
}