using System;

namespace Infotecs.Attika.AtticaDataModel.Repos
{
    public interface ICommandRepo
    {
        void CreateArticle(Article article);
        void CreateComment(Guid articleId, Comment comment);

        void DeleteArticle(string articleId);
        void DeleteComment(string commentId);
    }
}