using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;
using AttikaContracts.DataTransferObjects;
using AttikaContracts.Messages;
using Infotecs.Attika.AttikaDomain.Aggregates;
using Infotecs.Attika.AttikaDomain.Factories.Contracts;
using Infotecs.Attika.AttikaInfrastructure.Data.Models;
using Infotecs.Attika.AttikaInfrastructure.Data.Repositories;
using Infotecs.Attika.AttikaInfrastructure.Data.Repositories.Exceptions;
using Infotecs.Attika.AttikaInfrastructure.Services.Contracts;
using Nelibur.ServiceModel.Services.Operations;

namespace Infotecs.Attika.AttikaDomain.Services.RequestProcessors
{
    public sealed class ArticleQueryHandler :
        IGet<GetArticleHeadersRequest>,
        IGet<GetArticleRequest>
    {
        private readonly IArticleFactory _articleFactory;
        private readonly IMappingService _mappingService;
        private readonly IQueryRepository _queryRepository;

        public ArticleQueryHandler(
            IQueryRepository queryRepository,
            IMappingService mappingService,
            IArticleFactory articleFactory)
        {
            _queryRepository = queryRepository;
            _mappingService = mappingService;
            _articleFactory = articleFactory;
        }

        public object Get(GetArticleHeadersRequest request)
        {
            if (request == null)
            {
                throw new WebFaultException(HttpStatusCode.BadRequest);
            }

            IEnumerable<ArticleHeaderState> headers;
            try
            {
                headers = _queryRepository.GetHeaders();
            }
            catch (RepositoryException)
            {
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }

            try
            {
                IEnumerable<ArticleHeaderDto> mappedHeaders = headers.Select(_mappingService.Map<ArticleHeaderDto>);
                return new GetArticleHeadersResponse { Headers = mappedHeaders.ToList() };
            }
            catch (Exception)
            {
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

        public object Get(GetArticleRequest request)
        {
            if (request == null)
            {
                throw new WebFaultException(HttpStatusCode.BadRequest);
            }
            Guid guid;
            if (!Guid.TryParse(request.Id, out guid))
            {
                throw new WebFaultException(HttpStatusCode.BadRequest);
            }

            Article article;
            try
            {
                article = _articleFactory.CreateArticleFromRepository(guid);
            }
            catch (RepositoryException)
            {
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
            catch (ArgumentException)
            {
                throw new WebFaultException(HttpStatusCode.ExpectationFailed);
            }

            if (article == null)
            {
                throw new WebFaultException(HttpStatusCode.NotFound);
            }

            try
            {
                return new GetArticleResponse { Article = _mappingService.Map<ArticleDto>(article.State) };
            }
            catch (Exception)
            {
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }
    }
}
