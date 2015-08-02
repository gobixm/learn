using System;

namespace Infotecs.Attika.AttikaInfrastructure.Data.Models
{
    public class CommentState
    {
        public virtual Guid Id { get; set; }
        public virtual Guid ArticleId { get; set; }
        public virtual ArticleState ArticleState { get; set; }
        public virtual string Text { get; set; }
        public virtual DateTime Created { get; set; }
    }
}