using System;
using Infotecs.Attika.AttikaInfrastructure.Data.Models;

namespace Infotecs.Attika.AttikaInfrastructure.Data.Repositories
{
    public interface ICommandRepository
    {
        void CreateArticle(ArticleState articleState);
        void CreateComment(Guid articleId, CommentState commentState);
        void DeleteArticle(string articleId);
        void DeleteComment(string commentId);

        void UpdateArticle(ArticleState articleState);
    }
}