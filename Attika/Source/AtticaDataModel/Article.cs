using System;
using System.Collections.Generic;

namespace Infotecs.Attika.AtticaDataModel
{
    public class Article
    {
        private ICollection<Comment> _comments;

        public Article()
        {
            _comments = new List<Comment>();
        }

        public virtual Guid Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual string Text { get; set; }
        public virtual DateTime Created { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }
    }
}