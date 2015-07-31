using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Infotecs.Attika.AttikaSharedDataObjects.DataTransferObjects
{
    [DataContract]
    [TypeConverter(typeof (ArticleDtoConverter))]
    public sealed class ArticleDto
    {
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