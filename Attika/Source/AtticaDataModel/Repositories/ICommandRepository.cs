using System;

namespace Infotecs.Attika.AtticaDataModel.Repositories
{
    public interface ICommandRepository
    {
        void CreateArticle(Article article);
        void CreateComment(Guid articleId, Comment comment);

        void DeleteArticle(string articleId);
        void DeleteComment(string commentId);
    }
}