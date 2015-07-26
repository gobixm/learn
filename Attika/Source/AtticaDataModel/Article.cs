using System;
using System.Collections.Generic;

namespace Infotecs.Attika.AtticaDataModel
{
    public class Article
    {
        public virtual Guid Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Text { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual IList<Comment> Comments { get; set; }
    }
}