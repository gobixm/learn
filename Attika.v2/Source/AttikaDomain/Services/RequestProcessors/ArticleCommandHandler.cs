using System;
using System.Net;
using System.ServiceModel.Web;
using AttikaContracts.Messages;
using Infotecs.Attika.AttikaDomain.Factories.Contracts;
using Infotecs.Attika.AttikaDomain.Services.Queuing;
using Nelibur.ServiceModel.Services.Operations;

namespace Infotecs.Attika.AttikaDomain.Services.RequestProcessors
{
    public sealed class ArticleCommandHandler :
        IPostOneWay<NewArticleRequest>,
        IPostOneWay<AddArticleCommentRequest>,
        IDeleteOneWay<DeleteArticleRequest>,
        IDeleteOneWay<DeleteArticleCommentRequest>
    {
        private readonly IArticleFactory _articleFactory;
        private readonly ICommentFactory _commentFactory;

        private readonly IQueueService _queueService;

        public ArticleCommandHandler(
            IQueueService queueService,
            IArticleFactory articleFactory,
            ICommentFactory commentFactory)
        {
            _queueService = queueService;
            _articleFactory = articleFactory;
            _commentFactory = commentFactory;
        }

        public void DeleteOneWay(DeleteArticleCommentRequest request)
        {
            if (request == null)
            {
                throw new WebFaultException(HttpStatusCode.BadRequest);
            }

            Guid articleGuid;
            if (!Guid.TryParse(request.ArticleId, out articleGuid))
            {
                throw new WebFaultException(HttpStatusCode.BadRequest);
            }
            Guid commentGuid;
            if (!Guid.TryParse(request.CommentId, out commentGuid))
            {
                throw new WebFaultException(HttpStatusCode.BadRequest);
            }

            try
            {
                _queueService.PushMessage(request);
            }
            catch (Exception)
            {
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

        public void DeleteOneWay(DeleteArticleRequest request)
        {
            if (request == null)
            {
                throw new WebFaultException(HttpStatusCode.BadRequest);
            }

            Guid articleGuid;
            if (!Guid.TryParse(request.ArticleId, out articleGuid))
            {
                throw new WebFaultException(HttpStatusCode.BadRequest);
            }

            try
            {
                _queueService.PushMessage(request);
            }
            catch (Exception)
            {
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

        public void PostOneWay(AddArticleCommentRequest request)
        {
            if (request == null)
            {
                throw new WebFaultException(HttpStatusCode.BadRequest);
            }

            Guid articleGuid;
            if (!Guid.TryParse(request.ArticleId, out articleGuid))
            {
                throw new WebFaultException(HttpStatusCode.BadRequest);
            }

            //just ensure we're able to construct valid comment later
            try
            {
                _commentFactory.CreateComment(request.Comment);
            }
            catch (ArgumentException)
            {
                throw new WebFaultException(HttpStatusCode.ExpectationFailed);
            }
            try
            {
                _queueService.PushMessage(request);
            }
            catch (Exception)
            {
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

        public void PostOneWay(NewArticleRequest request)
        {
            if (request == null)
            {
                throw new WebFaultException(HttpStatusCode.BadRequest);
            }

            //just ensure we're able to construct valid article later
            try
            {
                _articleFactory.CreateArticle(request.Article);
            }
            catch (ArgumentException)
            {
                throw new WebFaultException(HttpStatusCode.ExpectationFailed);
            }
            try
            {
                _queueService.PushMessage(request);
            }
            catch (Exception)
            {
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }
    }
}