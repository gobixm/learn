using System;
using System.Collections.Generic;

namespace Infotecs.Attika.AtticaDataModel.Repositories
{
    public interface IQueryRepository
    {
        Article GetArticle(Guid articleId);
        IEnumerable<Comment> GetComments(Guid articleId);
        IEnumerable<ArticleHeader> GetHeaders();
    }
}