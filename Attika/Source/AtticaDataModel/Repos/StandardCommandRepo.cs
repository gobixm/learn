using System;

namespace Infotecs.Attika.AtticaDataModel.Repos
{
    public class StandardCommandRepo : ICommandRepo
    {
        public void CreateArticle(Article article)
        {
            using (var session = SessionHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    if (article.Id == Guid.Empty)
                    {
                        article.Id = Guid.NewGuid();
                    }
                    article.Created = DateTime.Now;
                    session.Save(article);
                    transaction.Commit();
                }
            }
        }

        public void CreateComment(Guid articleId, Comment comment)
        {
            using (var session = SessionHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    if (comment.Id == Guid.Empty)
                    {
                        comment.Id = Guid.NewGuid();
                    }
                    comment.Created = DateTime.Now;
                    comment.ArticleId = articleId;
                    session.Save(comment);
                    transaction.Commit();
                }
            }
        }
    }
}