using System;
using System.Collections.Generic;
using System.Linq;
using Infotecs.Attika.AttikaDomain.Aggregates;
using Infotecs.Attika.AttikaDomain.Entities;
using Infotecs.Attika.AttikaDomain.Factories.Contracts;
using Infotecs.Attika.AttikaDomain.Services.Exceptions;
using Infotecs.Attika.AttikaDomain.Services.Metadata;
using Infotecs.Attika.AttikaDomain.Services.Queuing;
using Infotecs.Attika.AttikaInfrastructure.Data.DataTransferObjects;
using Infotecs.Attika.AttikaInfrastructure.Data.Models;
using Infotecs.Attika.AttikaInfrastructure.Data.Repositories;
using Infotecs.Attika.AttikaInfrastructure.Data.Repositories.Exceptions;
using Infotecs.Attika.AttikaInfrastructure.Messaging.Messages;
using Infotecs.Attika.AttikaInfrastructure.Messaging.Serializers;
using Infotecs.Attika.AttikaInfrastructure.Services.Contracts;
using NLog;

namespace Infotecs.Attika.AttikaDomain.Services.RequestProcessors
{
    public sealed class ArticleHandler : BaseHandler
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IArticleFactory _articleFactory;
        private readonly ICommandRepository _commandRepository;
        private readonly ICommentFactory _commentFactory;
        private readonly IMappingService _mappingService;
        private readonly IMessageSerializationService _messageSerializationService;
        private readonly IQueryRepository _queryRepository;
        private readonly IQueueService _queue;

        public ArticleHandler(IQueryRepository queryRepository, ICommandRepository commandRepository,
                              IMappingService mappingService,
                              IQueueService queue, IArticleFactory articleFactory, ICommentFactory commentFactory,
                              IMessageSerializationService messageSerializationService)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
            _mappingService = mappingService;
            _queue = queue;
            _articleFactory = articleFactory;
            _commentFactory = commentFactory;
            _messageSerializationService = messageSerializationService;
        }

        public override object Clone()
        {
            var handler = new ArticleHandler(_queryRepository, _commandRepository, _mappingService, _queue,
                                             _articleFactory, _commentFactory, _messageSerializationService);
            return handler;
        }

        public GetArticleResponse Handle(GetArticleRequest getArticleRequest)
        {
            if (getArticleRequest == null)
            {
                throw new ServiceException(ServiceMetadata.RequestIsEmptyError);
            }
            Guid guid;
            if (!Guid.TryParse(getArticleRequest.Id, out guid))
            {
                throw new ServiceException(ServiceMetadata.BadRequestError);
            }

            Article article;
            try
            {
                article = _articleFactory.CreateArticleFromRepository(guid);
            }
            catch (RepositoryException ex)
            {
                throw new ServiceException(ServiceMetadata.InternalServiceError, ex);
            }
            catch (ArgumentException ex)
            {
                throw new ServiceException(ex.Message, ex);
            }

            if (article == null)
            {
                throw new ServiceException(ServiceMetadata.ArticleNotFoundError);
            }

            try
            {
                return new GetArticleResponse {Article = _mappingService.Map<ArticleDto>(article.State)};
            }
            catch (Exception ex)
            {
                throw new ServiceException(ServiceMetadata.InternalServiceError, ex);
            }
        }

        public GetArticleHeadersResponse Handle(GetArticleHeadersRequest getArticleHeadersRequest)
        {
            if (getArticleHeadersRequest == null)
            {
                throw new ServiceException(ServiceMetadata.RequestIsEmptyError);
            }

            IEnumerable<ArticleHeaderState> headers;
            try
            {
                headers = _queryRepository.GetHeaders();
            }
            catch (RepositoryException ex)
            {
                throw new ServiceException(ServiceMetadata.InternalServiceError, ex);
            }

            try
            {
                IEnumerable<ArticleHeaderDto> mappedHeaders = headers.Select(_mappingService.Map<ArticleHeaderDto>);
                return new GetArticleHeadersResponse {Headers = mappedHeaders.ToList()};
            }
            catch (Exception ex)
            {
                throw new ServiceException(ServiceMetadata.InternalServiceError, ex);
            }
        }

        public void Enqueue(NewArticleRequest newArticleRequest)
        {
            if (newArticleRequest == null)
            {
                throw new ServiceException(ServiceMetadata.RequestIsEmptyError);
            }

            //just ensure we're able to construct valid article later
            try
            {
                _articleFactory.CreateArticle(newArticleRequest.Article);
            }
            catch (ArgumentException ex)
            {
                throw new ServiceException(ex.Message, ex);
            }
            try
            {
                _queue.PushMessage(_messageSerializationService.Serialize(newArticleRequest));
            }
            catch (Exception ex)
            {
                throw new ServiceException(ServiceMetadata.InternalServiceError, ex);
            }
        }

        public object Handle(NewArticleRequest newArticleRequest)
        {
            try
            {
                Article article = _articleFactory.CreateArticle(newArticleRequest.Article);
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

            return null;
        }

        public void Enqueue(AddArticleCommentRequest addArticleCommentRequest)
        {
            if (addArticleCommentRequest == null)
            {
                throw new ServiceException(ServiceMetadata.RequestIsEmptyError);
            }

            Guid articleGuid;
            if (!Guid.TryParse(addArticleCommentRequest.ArticleId, out articleGuid))
            {
                throw new ServiceException(ServiceMetadata.BadRequestError);
            }

            Article article;
            try
            {
                article = _articleFactory.CreateArticleFromRepository(articleGuid);
            }
            catch (RepositoryException ex)
            {
                throw new ServiceException(ServiceMetadata.InternalServiceError, ex);
            }

            if (article == null)
            {
                throw new ServiceException(ServiceMetadata.ArticleNotFoundError);
            }

            //just ensure we're able to construct valid comment later
            try
            {
                _commentFactory.CreateComment(addArticleCommentRequest.Comment);
            }
            catch (ArgumentException ex)
            {
                throw new ServiceException(ex.Message, ex);
            }
            try
            {
                _queue.PushMessage(_messageSerializationService.Serialize(addArticleCommentRequest));
            }
            catch (Exception ex)
            {
                throw new ServiceException(ServiceMetadata.InternalServiceError, ex);
            }
        }

        public object Handle(AddArticleCommentRequest addArticleCommentRequest)
        {
            Article article = null;
            try
            {
                article = _articleFactory.CreateArticleFromRepository(Guid.Parse(addArticleCommentRequest.ArticleId));
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
                Logger.Warn("Статья с id {0} не найдена.", addArticleCommentRequest.ArticleId);
                return null;
            }

            Comment comment = null;
            try
            {
                comment = _commentFactory.CreateComment(addArticleCommentRequest.Comment);
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

            return null;
        }
    }
}