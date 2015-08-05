using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AttikaContracts.DataTransferObjects
{
    [DataContract]
    public sealed class ArticleDto
    {
        public ArticleDto()
        {
            Comments = new List<CommentDto>();
            Title = "";
            Description = "";
            Text = "";
        }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public DateTime Created { get; set; }

        [DataMember]
        public List<CommentDto> Comments { get; set; }
    }
}