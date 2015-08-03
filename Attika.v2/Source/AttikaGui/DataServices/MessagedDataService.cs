using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Reflection;
using Infotecs.Attika.AttikaGui.DataTransferObjects;
using Infotecs.Attika.AttikaGui.Messages.Wcf;

namespace Infotecs.Attika.AttikaGui.DataServices
{
    public sealed class MessagedDataService : IDataService
    {
        private readonly IDataSerializer _responseSerializer;
        private readonly WebClient _webClient;

        public MessagedDataService(IDataSerializer responseSerializer)
        {
            _responseSerializer = responseSerializer;
            _webClient = new WebClient {BaseAddress = ConfigurationManager.ConnectionStrings["host"].ConnectionString};
            _webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
        }

        public IEnumerable<ArticleHeaderDto> GetArticleHeaders()
        {
            try
            {
                string url = "/api/get?" + BuildQueryParams(new {Request = "Article.GetArticleHeadersRequest"});
                byte[] response = _webClient.DownloadData(url);
                var message = _responseSerializer.Deserialize<GetArticleHeadersResponse>(response);
                return message.Headers;
            }
            catch (WebException ex)
            {
                throw new DataServiceException(GetFaultDto(ex));
            }
        }

        public ArticleDto GetArticle(string articleId)
        {
            try
            {
                string url = "/api/get?" + BuildQueryParams(new {Request = "Article.GetArticleRequest", Id = articleId});
                byte[] response = _webClient.DownloadData(url);
                var message = _responseSerializer.Deserialize<GetArticleResponse>(response);
                return message.Article;
            }
            catch (WebException ex)
            {
                throw new DataServiceException(GetFaultDto(ex));
            }
        }

        public void NewArticle(ArticleDto article)
        {
            _webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            try
            {
                string url = "/api/post?" + BuildQueryParams(new {Request = "Article.NewArticleRequest"});
                _webClient.UploadData(url, "POST", _responseSerializer.Serialize(new {Article = article}));
            }
            catch (WebException ex)
            {
                throw new DataServiceException(GetFaultDto(ex));
            }
        }

        public void NewComment(string articleId, CommentDto comment)
        {
            _webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            try
            {
                string url = "/api/post?" + BuildQueryParams(new {Request = "Article.AddArticleCommentRequest"});
                _webClient.UploadData(url, "POST",
                                      _responseSerializer.Serialize(new {ArticleId = articleId, Comment = comment}));
            }
            catch (WebException ex)
            {
                throw new DataServiceException(GetFaultDto(ex));
            }
        }

        public void DeleteArticle(string articleId)
        {
            try
            {
                _webClient.UploadString("/Article/" + articleId + "/Delete", "DELETE", "");
            }
            catch (WebException ex)
            {
                throw new DataServiceException(GetFaultDto(ex));
            }
        }

        public void DeleteComment(string commentId)
        {
            try
            {
                _webClient.UploadString("/Comment/" + commentId + "/Delete", "DELETE", "");
            }
            catch (WebException ex)
            {
                throw new DataServiceException(GetFaultDto(ex));
            }
        }

        private FaultDto GetFaultDto(WebException exception)
        {
            if (exception.Response == null)
            {
                return new FaultDto {Message = "Ошибка при обращении к серверу", Detail = exception.Message};
            }
            using (var ms = new MemoryStream())
            {
                using (Stream responseStream = exception.Response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        responseStream.CopyTo(ms);
                        return _responseSerializer.Deserialize<FaultDto>(ms.GetBuffer());
                    }
                    return null;
                }
            }
        }

        private string BuildQueryParams(object message)
        {
            var pairs = new List<string>();
            foreach (PropertyInfo property in message.GetType().GetProperties())
            {
                pairs.Add(string.Format("{0}={1}", property.Name, property.GetValue(message)));
            }
            return string.Join("&", pairs);
        }
    }
}