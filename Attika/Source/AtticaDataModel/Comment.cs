using System;

namespace Infotecs.Attika.AtticaDataModel
{
    public class Comment
    {
        public virtual Guid Id { get; set; }
        public virtual Guid ArticleId { get; set; }
        public virtual string Text { get; set; }
        public virtual DateTime Created { get; set; }
    }
}