using System.Collections.Generic;
using System.Configuration;
using System.Net;
using Infotecs.Attika.AttikaGui.DTO;

namespace Infotecs.Attika.AttikaGui.Model
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
            byte[] response = _webClient.DownloadData("/Article");
            return _serializer.Deserialize<List<ArticleHeaderDto>>(response);
        }

        public ArticleDto GetArticle(string articleId)
        {
            byte[] response = _webClient.DownloadData("/Article/" + articleId);
            return _serializer.Deserialize<ArticleDto>(response);
        }

        public void NewArticle(ArticleDto article)
        {
            _webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            _webClient.UploadData("/Article/New", "POST", _serializer.Serialize(article));
        }

        public void NewComment(string articleId, CommentDto comment)
        {
            _webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            _webClient.UploadData("/Article/" + articleId + "/Comment/New", "POST",
                                  _serializer.Serialize(new {articleId, commentDto = comment}));
        }

        public void DeleteArticle(string articleId)
        {
            _webClient.UploadString("/Article/" + articleId + "/Delete", "DELETE", "");
        }

        public void DeleteComment(string commentId)
        {
            _webClient.UploadString("/Comment" + commentId + "/Delete", "DELETE", "");
        }
    }
}