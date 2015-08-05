using System;
using AttikaContracts.Messages;
using Infotecs.Attika.AttikaDomain.Aggregates;
using Infotecs.Attika.AttikaDomain.Entities;
using Infotecs.Attika.AttikaDomain.Factories.Contracts;
using Infotecs.Attika.AttikaDomain.Services.Queuing;
using Infotecs.Attika.AttikaInfrastructure.Data.Repositories;
using Infotecs.Attika.AttikaInfrastructure.Data.Repositories.Exceptions;
using NLog;

namespace Infotecs.Attika.AttikaDomain.Services.RequestProcessors
{
    public class ArticleQueueProcessor :
        IQueueProcessor<NewArticleRequest>,
        IQueueProcessor<AddArticleCommentRequest>,
        IQueueProcessor<DeleteArticleRequest>,
        IQueueProcessor<DeleteArticleCommentRequest>

    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IArticleFactory _articleFactory;
        private readonly ICommandRepository _commandRepository;
        private readonly ICommentFactory _commentFactory;

        public ArticleQueueProcessor(IArticleFactory articleFactory, ICommentFactory commentFactory,
                                     ICommandRepository commandRepository)
        {
            _articleFactory = articleFactory;
            _commentFactory = commentFactory;
            _commandRepository = commandRepository;
        }

        public void Process(AddArticleCommentRequest request)
        {
            Article article = null;
            try
            {
                article = _articleFactory.CreateArticleFromRepository(Guid.Parse(request.ArticleId));
            }
            catch (RepositoryException ex)
            {
                Logger.Warn(ex, "Ошибка репозитория.");
            }
            catch (ArgumentException ex)
            {
                Logger.Warn(ex, "Ошибка валидации создании статьи.");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Неизвестная ошибка при получении статьи по идентификатору.");
            }

            if (article == null)
            {
                Logger.Warn("Статья с id {0} не найдена.", request.ArticleId);
                return;
            }

            Comment comment = null;
            try
            {
                comment = _commentFactory.CreateComment(request.Comment);
            }
            catch (ArgumentException ex)
            {
                Logger.Warn(ex, "Ошибка валидации комментария.");
            }

            article.AddComment(comment);
            try
            {
                _commandRepository.UpdateArticle(article.State);
            }
            catch (RepositoryException ex)
            {
                Logger.Warn(ex, "Ошибка репозитория при обновлении статьи.");
            }
        }

        public void Process(DeleteArticleCommentRequest request)
        {
            Article article = null;
            try
            {
                article = _articleFactory.CreateArticleFromRepository(Guid.Parse(request.ArticleId));
            }
            catch (RepositoryException ex)
            {
                Logger.Warn(ex, "Ошибка репозитория.");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Неизвестная ошибка при получении статьи по идентификатору.");
            }

            if (article == null)
            {
                Logger.Warn("Статья с id {0} не найдена.", request.ArticleId);
                return;
            }

            article.DeleteComment(Guid.Parse(request.CommentId));
            try
            {
                _commandRepository.UpdateArticle(article.State);
            }
            catch (RepositoryException ex)
            {
                Logger.Warn(ex, "Ошибка репозитория при обновлении статьи.");
            }
        }

        public void Process(DeleteArticleRequest request)
        {
            try
            {
                _commandRepository.DeleteArticle(request.ArticleId);
            }
            catch (RepositoryException ex)
            {
                Logger.Warn(ex, "Ошибка репозитория.");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Неизвестная ошибка при удалении статьи по идентификатору.");
            }
        }

        public void Process(NewArticleRequest request)
        {
            try
            {
                Article article = _articleFactory.CreateArticle(request.Article);
                _commandRepository.CreateArticle(article.State);
            }
            catch (ArgumentException ex)
            {
                Logger.Warn(ex, "Ошибка валидации создании статьи.");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Неизвестная ошибка при создании статьи.");
            }
        }
    }
}