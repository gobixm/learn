using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;
using Infotecs.Attika.AtticaDataModel;
using Infotecs.Attika.AtticaDataModel.Repositories;
using Infotecs.Attika.AttikaService.DataTransferObjects;
using Infotecs.Attika.AttikaService.Mappings;

namespace Infotecs.Attika.AttikaService.Messages.Handlers
{
    public sealed class ArticleHandler : BaseHandler
    {
        private readonly ICommandRepository _commandRepository;
        private readonly IMapper _mapper;
        private readonly IQueryRepository _queryRepository;

        public ArticleHandler(IQueryRepository queryRepository, ICommandRepository commandRepository, IMapper mapper)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
            _mapper = mapper;
        }

        public override object Clone()
        {
            var handler = new ArticleHandler(_queryRepository, _commandRepository, _mapper);
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
    }
}