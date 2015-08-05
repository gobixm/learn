using System;
using AttikaContracts.DataTransferObjects;
using Infotecs.Attika.AttikaDomain.Aggregates;

namespace Infotecs.Attika.AttikaDomain.Factories.Contracts
{
    public interface IArticleFactory
    {
        Article CreateArticleFromRepository(Guid id);
        Article CreateArticle(ArticleDto articleDto);
    }
}