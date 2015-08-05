using System;
using AttikaContracts.DataTransferObjects;
using Infotecs.Attika.AttikaDomain.Aggregates;

namespace Infotecs.Attika.AttikaDomain.Factories.Contracts
{
    public interface IArticleFactory
    {
        Article CreateArticle(ArticleDto articleDto);
        Article CreateArticleFromRepository(Guid id);
    }
}
