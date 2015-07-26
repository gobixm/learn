using System;

namespace Infotecs.Attika.AtticaDataModel.Repos
{
    public interface ICommandRepo
    {
        void CreateArticle(Article article);
        void CreateComment(Guid articleId, Comment comment);
    }
}