using System;
using System.Collections.Generic;

namespace Infotecs.Attika.AtticaDataModel.Repos
{
    public interface IQueryRepo
    {
        Article GetArticle(Guid articleId);
        IEnumerable<Comment> GetComments(Guid articleId);
        IEnumerable<ArticleHeader> GetHeaders();
    }
}