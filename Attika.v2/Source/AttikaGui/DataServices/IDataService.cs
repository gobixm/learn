using System;
using System.Collections.Generic;
using AttikaContracts.DataTransferObjects;

namespace Infotecs.Attika.AttikaGui.DataServices
{
    public interface IDataService
    {
        void DeleteArticle(string articleId);
        void DeleteComment(string articleId, string commentId);
        ArticleDto GetArticle(string articleId);
        IEnumerable<ArticleHeaderDto> GetArticleHeaders();
        void NewArticle(ArticleDto article);
        void NewComment(string articleId, CommentDto comment);
    }
}
