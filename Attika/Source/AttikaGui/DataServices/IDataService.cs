﻿using System.Collections.Generic;
using Infotecs.Attika.AttikaGui.DataTransferObjects;

namespace Infotecs.Attika.AttikaGui.DataServices
{
    public interface IDataService
    {
        IEnumerable<ArticleHeaderDto> GetArticleHeaders();
        ArticleDto GetArticle(string articleId);
        void NewArticle(ArticleDto article);
        void NewComment(string articleId, CommentDto comment);
        void DeleteArticle(string articleId);
        void DeleteComment(string commentId);
    }
}