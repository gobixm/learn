using System;
using System.Collections.Generic;
using Infotecs.Attika.AttikaInfrastructure.Data.Models;

namespace Infotecs.Attika.AttikaInfrastructure.Data.Repositories
{
    public interface IQueryRepository
    {
        ArticleState GetArticle(Guid articleId);
        IEnumerable<ArticleHeaderState> GetHeaders();
    }
}