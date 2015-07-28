using System;

namespace Infotecs.Attika.AttikaGui.DTO
{
    public sealed class CommentDto
    {
        public Guid Id { get; set; }
        public Guid ArticleId { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
    }
}