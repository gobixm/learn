using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;
using Infotecs.Attika.AtticaDataModel;
using Infotecs.Attika.AtticaDataModel.Repositories;
using Infotecs.Attika.AttikaService.Messages.Queues;
using Infotecs.Attika.AttikaService.Validators;
using Infotecs.Attika.AttikaSharedDataObjects.DataTransferObjects;
using Infotecs.Attika.AttikaSharedDataObjects.Mappings;
using Infotecs.Attika.AttikaSharedDataObjects.Messages;

namespace Infotecs.Attika.AttikaService.Messages.Handlers
{
    public sealed class ArticleHandler : BaseHandler
    {
        private readonly ICommandRepository _commandRepository;
        private readonly IMapper _mapper;
        private readonly IQueryRepository _queryRepository;
        private readonly IQueuePusher _queue;

        public ArticleHandler(IQueryRepository queryRepository, ICommandRepository commandRepository, IMapper mapper,
                              IQueuePusher queue)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
            _mapper = mapper;
            _queue = queue;
        }

        public override object Clone()
        {
            var handler = new ArticleHandler(_queryRepository, _commandRepository, _mapper, _queue);
            return handler;
        }

        public GetArticleResponse Handle(GetArticleRequest getArticleRequest)
        {
            Article article;
            try
            {
                article = _queryRepository.GetArticle(Guid.Parse(getArticleRequest.Id));
            }
            catch (Exception ex)
            {
                throw new WebFaultException<WebFaultDto>(
                    new WebFaultDto("Ошибка при получении статьи из репозитория", ex.Message),
                    HttpStatusCode.InternalServerError);
            }
            if (article == null)
            {
                throw new WebFaultException<WebFaultDto>(
                    new WebFaultDto("Ошибка при получении статьи",
                                    "Статьи с ID=" + getArticleRequest.Id + " не существует"),
                    HttpStatusCode.BadRequest);
            }
            try
            {
                return new GetArticleResponse {Article = _mapper.Map<ArticleDto>(article)};
            }
            catch (Exception ex)
            {
                throw new WebFaultException<WebFaultDto>(
                    new WebFaultDto("Ошибка при маппинге статьи", ex.Message),
                    HttpStatusCode.InternalServerError);
            }
        }

        public GetArticleHeadersResponse Handle(GetArticleHeadersRequest getArticleHeadersRequest)
        {
            IEnumerable<ArticleHeader> headers;
            try
            {
                headers = _queryRepository.GetHeaders();
            }
            catch (Exception ex)
            {
                throw new WebFaultException<WebFaultDto>(
                    new WebFaultDto("Ошибка при получении списка статей из репозитория", ex.Message),
                    HttpStatusCode.InternalServerError);
            }

            try
            {
                IEnumerable<ArticleHeaderDto> mappedHeaders = headers.Select(_mapper.Map<ArticleHeaderDto>);
                return new GetArticleHeadersResponse {Headers = mappedHeaders.ToList()};
            }
            catch (Exception ex)
            {
                throw new WebFaultException<WebFaultDto>(
                    new WebFaultDto("Ошибка при маппинге списка статей", ex.Message),
                    HttpStatusCode.InternalServerError);
            }
        }

        public object Handle(NewArticleRequest newArticleRequest)
        {
            string[] validationErrors;
            if (!newArticleRequest.Article.Validate(out validationErrors, true))
            {
                throw new WebFaultException<WebFaultDto>(
                    new WebFaultDto("Не удалось добавить статью", string.Join("\n", validationErrors)),
                    HttpStatusCode.BadRequest);
            }
            _queue.PushMessage(newArticleRequest);
            return null;
        }
    }
}