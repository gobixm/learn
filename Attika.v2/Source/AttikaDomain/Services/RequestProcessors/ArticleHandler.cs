using System;
using System.Collections.Generic;
using System.Linq;
using Infotecs.Attika.AttikaDomain.Aggregates;
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

namespace Infotecs.Attika.AttikaDomain.Services.RequestProcessors
{
    public sealed class ArticleHandler : BaseHandler
    {
        private readonly IArticleFactory _articleFactory;
        private readonly ICommandRepository _commandRepository;
        private readonly IMappingService _mappingService;
        private readonly IMessageSerializer _messageSerializer;
        private readonly IQueryRepository _queryRepository;
        private readonly IQueueService _queue;

        public ArticleHandler(IQueryRepository queryRepository, ICommandRepository commandRepository,
            IMappingService mappingService,
            IQueueService queue, IArticleFactory articleFactory, IMessageSerializer messageSerializer)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
            _mappingService = mappingService;
            _queue = queue;
            _articleFactory = articleFactory;
            _messageSerializer = messageSerializer;
        }

        public override object Clone()
        {
            var handler = new ArticleHandler(_queryRepository, _commandRepository, _mappingService, _queue,
                _articleFactory, _messageSerializer);
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
                article = _articleFactory.CreateArticle(guid);
            }
            catch (RepositoryException ex)
            {
                throw new ServiceException(ServiceMetadata.InternalServiceError, ex);
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
                var mappedHeaders = headers.Select(_mappingService.Map<ArticleHeaderDto>);
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
            _queue.PushMessage(_messageSerializer.Serialize(newArticleRequest));
        }

        public object Handle(NewArticleRequest newArticleRequest)
        {
            var article = _articleFactory.CreateArticle(newArticleRequest.Article);
            _commandRepository.CreateArticle(article.State);
            return null;
        }
    }
}