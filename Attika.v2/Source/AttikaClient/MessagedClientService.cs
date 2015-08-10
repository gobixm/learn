using System;
using System.Collections.Generic;
using System.Configuration;
using AttikaContracts.DataTransferObjects;
using AttikaContracts.Messages;
using Nelibur.ServiceModel.Clients;

namespace Infotecs.Attika.AttikaClient
{
    public sealed class MessagedClientService : IClientService, IDisposable
    {
        private readonly JsonServiceClient _webClient;

        public MessagedClientService() : this(ConfigurationManager.ConnectionStrings["host"].ConnectionString)
        {
        }

        public MessagedClientService(string connectionString)
        {
            _webClient = new JsonServiceClient(connectionString);
        }

        public void DeleteArticle(string articleId)
        {
            try
            {
                _webClient.Delete(new DeleteArticleRequest { ArticleId = articleId });
            }
            catch (Exception ex)
            {
                throw new DataServiceException(new FaultDto(ex.Message, ex.ToString()));
            }
        }

        public void DeleteComment(string articleId, string commentId)
        {
            try
            {
                _webClient.Delete(new DeleteArticleCommentRequest { ArticleId = articleId, CommentId = commentId });
            }
            catch (Exception ex)
            {
                throw new DataServiceException(new FaultDto(ex.Message, ex.ToString()));
            }
        }

        public ArticleDto GetArticle(string articleId)
        {
            try
            {
                var response = _webClient.Get<GetArticleResponse>(new GetArticleRequest { Id = articleId });
                return response.Article;
            }
            catch (Exception ex)
            {
                throw new DataServiceException(new FaultDto(ex.Message, ex.ToString()));
            }
        }

        public IEnumerable<ArticleHeaderDto> GetArticleHeaders()
        {
            try
            {
                var response = _webClient.Get<GetArticleHeadersResponse>(new GetArticleHeadersRequest());
                return response.Headers;
            }
            catch (Exception ex)
            {
                throw new DataServiceException(new FaultDto(ex.Message, ex.ToString()));
            }
        }

        public void NewArticle(ArticleDto article)
        {
            try
            {
                _webClient.Post(new NewArticleRequest { Article = article });
            }
            catch (Exception ex)
            {
                throw new DataServiceException(new FaultDto(ex.Message, ex.ToString()));
            }
        }

        public void NewComment(string articleId, CommentDto comment)
        {
            try
            {
                _webClient.Post(new AddArticleCommentRequest { ArticleId = articleId, Comment = comment });
            }
            catch (Exception ex)
            {
                throw new DataServiceException(new FaultDto(ex.Message, ex.ToString()));
            }
        }

        public void Dispose()
        {
            _webClient.Dispose();
        }
    }
}
