using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using Infotecs.Attika.AttikaGui.DTO;
using Newtonsoft.Json;

namespace Infotecs.Attika.AttikaGui.Model
{
    public class DataService : IDataService
    {
        private readonly WebClient _webClient;

        public DataService()
        {
            _webClient = new WebClient {BaseAddress = ConfigurationManager.ConnectionStrings["host"].ConnectionString};
            _webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
        }
        public IEnumerable<ArticleHeaderDto> GetArticleHeaders()
        {
            string response = _webClient.DownloadString("/Article/Get");
            return JsonConvert.DeserializeObject<List<ArticleHeaderDto>>(response);
        }

        public ArticleDto GetArticle(string articleId)
        {
            throw new NotImplementedException();
        }

        public void NewArticle(ArticleDto article)
        {
            _webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            _webClient.UploadData(new Uri(_webClient.BaseAddress + "/Article/New"), Serialize<ArticleDto>(article));
        }

        public void NewComment(string articleId, CommentDto comment)
        {
            _webClient.UploadString(new Uri(_webClient.BaseAddress + "/Article/NewComment"), "POST", JsonConvert.SerializeObject(new {ArticleId = articleId, CommentDto = comment}));
        }

        private byte[] Serialize<T>(T dto)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, dto);
            return ms.ToArray();
        }
    }
}