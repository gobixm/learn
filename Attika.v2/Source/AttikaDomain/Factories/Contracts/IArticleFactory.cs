using System;
using Infotecs.Attika.AttikaDomain.Aggregates;
using Infotecs.Attika.AttikaInfrastructure.Data.DataTransferObjects;

namespace Infotecs.Attika.AttikaDomain.Factories.Contracts
{
    public interface IArticleFactory
    {
        Article CreateArticleFromRepository(Guid id);
        Article CreateArticle(ArticleDto articleDto);
    }
}