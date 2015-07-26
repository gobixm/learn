using System;

namespace Infotecs.Attika.AttikaGui.DTO
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public Guid ArticleId { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
    }
}