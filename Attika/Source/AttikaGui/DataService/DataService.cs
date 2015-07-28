using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using Infotecs.Attika.AttikaGui.DTO;

namespace Infotecs.Attika.AttikaGui.DataService
{
    public sealed class DataService : IDataService
    {
        private readonly IDataSerializer _serializer;
        private readonly WebClient _webClient;

        public DataService(IDataSerializer serializer)
        {
            _serializer = serializer;
            _webClient = new WebClient {BaseAddress = ConfigurationManager.ConnectionStrings["host"].ConnectionString};
            _webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
        }

        public IEnumerable<ArticleHeaderDto> GetArticleHeaders()
        {
            try
            {
                byte[] response = _webClient.DownloadData("/Article");
                return _serializer.Deserialize<List<ArticleHeaderDto>>(response);
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
                byte[] response = _webClient.DownloadData("/Article/" + articleId);
                return _serializer.Deserialize<ArticleDto>(response);
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
                _webClient.UploadData("/Article/New", "POST", _serializer.Serialize(article));
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
                _webClient.UploadData("/Article/" + articleId + "/Comment/New", "POST",
                                      _serializer.Serialize(new {articleId, commentDto = comment}));
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
                        return _serializer.Deserialize<FaultDto>(ms.GetBuffer());
                    }
                    return null;
                }
            }
        }
    }
}