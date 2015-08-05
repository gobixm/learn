using System;
using Infotecs.Attika.AttikaInfrastructure.Data.Models;

namespace Infotecs.Attika.AttikaInfrastructure.Data.Repositories
{
    public interface ICommandRepository
    {
        void CreateArticle(ArticleState articleState);
        void DeleteArticle(string articleId);
        void UpdateArticle(ArticleState articleState);
    }
}
