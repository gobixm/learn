using System;
using NHibernate;

namespace Infotecs.Attika.AtticaDataModel.Repos
{
    public sealed class StandardCommandRepo : ICommandRepo
    {
        public void CreateArticle(Article article)
        {
            using (ISession session = SessionHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
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
            using (ISession session = SessionHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
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


        public void DeleteArticle(string articleId)
        {
            using (ISession session = SessionHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(session.Load<Article>(Guid.Parse(articleId)));
                    transaction.Commit();
                }
            }
        }

        public void DeleteComment(string commentId)
        {
            using (ISession session = SessionHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(session.Load<Comment>(Guid.Parse(commentId)));
                    transaction.Commit();
                }
            }
        }
    }
}