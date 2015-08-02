using System;
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
                using (var session = SessionHelper.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        foreach (var comment in articleState.Comments)
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

        public void CreateComment(Guid articleId, CommentState commentState)
        {
            try
            {
                using (var session = SessionHelper.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        commentState.Created = DateTime.Now;
                        commentState.ArticleState = session.Get<ArticleState>(articleId);
                        session.Save(commentState);
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
                using (var session = SessionHelper.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        session.Delete(session.Load<ArticleState>(Guid.Parse(articleId)));
                        transaction.Commit();
                    }
                }
            }
            catch (HibernateException ex)
            {
                throw new RepositoryException("Ошибка репозитория", ex);
            }
        }

        public void DeleteComment(string commentId)
        {
            try
            {
                using (var session = SessionHelper.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        session.Delete(session.Load<CommentState>(Guid.Parse(commentId)));
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
                using (var session = SessionHelper.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        var existingArticle = session.Load<ArticleState>(articleState.Id);
                        foreach (var comment in articleState.Comments)
                        {
                            session.Merge(comment);
                        }
                        var s = session.Merge(articleState);
                        session.Update(s);
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