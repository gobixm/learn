using System;
using System.Linq;
using Infotecs.Attika.AttikaInfrastructure.Data.Models;
using Infotecs.Attika.AttikaInfrastructure.Data.Repositories.Exceptions;
using NHibernate;

namespace Infotecs.Attika.AttikaInfrastructure.Data.Repositories
{
    public sealed class StandardCommandRepository : ICommandRepository
    {
        public void CreateArticle(ArticleState articleState)
        {
            try
            {
                using (ISession session = SessionHelper.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        foreach (CommentState comment in articleState.Comments)
                        {
                            session.Save(comment);
                        }
                        session.Save(articleState);
                        transaction.Commit();
                    }
                }
            }
            catch (HibernateException ex)
            {
                throw new RepositoryException("Ошибка репозитория", ex);
            }
        }

        public void DeleteArticle(string articleId)
        {
            try
            {
                using (ISession session = SessionHelper.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        var article = session.Load<ArticleState>(Guid.Parse(articleId));
                        foreach (CommentState comment in article.Comments)
                        {
                            session.Delete(comment);
                        }
                        session.Delete(article);
                        transaction.Commit();
                    }
                }
            }
            catch (HibernateException ex)
            {
                throw new RepositoryException("Ошибка репозитория", ex);
            }
        }

        public void UpdateArticle(ArticleState articleState)
        {
            try
            {
                using (ISession session = SessionHelper.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        var article = session.Load<ArticleState>(articleState.Id);
                        foreach (CommentState comment in article.Comments)
                        {
                            CommentState existingComment = articleState.Comments.FirstOrDefault(c => c.Id == comment.Id);
                            if (existingComment == null)
                            {
                                session.Delete(comment);
                            }
                        }
                        foreach (CommentState comment in articleState.Comments)
                        {
                            comment.ArticleState = article;
                            session.Merge(comment);
                        }
                        ArticleState loadedState = session.Merge(articleState);
                        session.Update(loadedState);
                        transaction.Commit();
                    }
                }
            }
            catch (HibernateException ex)
            {
                throw new RepositoryException("Ошибка репозитория", ex);
            }
        }
    }
}
