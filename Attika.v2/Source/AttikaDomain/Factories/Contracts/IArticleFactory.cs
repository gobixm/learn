using System;
using Infotecs.Attika.AttikaDomain.Aggregates;
using Infotecs.Attika.AttikaInfrastructure.Data.DataTransferObjects;

namespace Infotecs.Attika.AttikaDomain.Factories.Contracts
{
    public interface IArticleFactory
    {
        Article CreateArticle(Guid id);
        Article NewArticle(string title, string description, string text);
        Article CreateArticle(ArticleDto articleDto);
    }
}