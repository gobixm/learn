using System;
using System.Linq;
using Infotecs.Attika.AttikaInfrastructure.Data.Models;
using Infotecs.Attika.AttikaInfrastructure.Data.Repositories.Exceptions;
using NHibernate;
using NHibernate.Criterion;

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
                        var article = session.Get<ArticleState>(Guid.Parse(articleId));
                        article.Comments.Clear();
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
                        var criteria = session.CreateCriteria<ArticleState>().SetFetchMode("Comments", FetchMode.Eager);
                        criteria.Add(Restrictions.Eq("Id", articleState.Id));
                        var article = criteria.List<ArticleState>().FirstOrDefault();
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
