using System;
using System.Collections.Generic;
using System.ComponentModel;
using Infotecs.Attika.AttikaInfrastructure.Data.Mappings;

namespace Infotecs.Attika.AttikaInfrastructure.Data.Models
{
    [TypeConverter(typeof(ArticleStateConverter))]
    public class ArticleState
    {
        private ICollection<CommentState> _comments;

        public ArticleState()
        {
            _comments = new List<CommentState>();
        }

        public virtual ICollection<CommentState> Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }

        public virtual DateTime Created { get; set; }

        public virtual string Description { get; set; }
        public virtual Guid Id { get; set; }
        public virtual string Text { get; set; }
        public virtual string Title { get; set; }
    }
}
