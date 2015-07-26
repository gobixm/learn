using System;
using System.Collections.Generic;

namespace Infotecs.Attika.AttikaGui.DTO
{
    public sealed class ArticleDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public List<CommentDto> Comments { get; set; }
    }
}