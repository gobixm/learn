using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Infotecs.Attika.AttikaService.DTO;

namespace Infotecs.Attika.AttikaService
{
    [ServiceContract]
    public interface IArticleService
    {
        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "Article/New",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        void NewArticle(ArticleDto articleDto);

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "Article/NewComment",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        void AddComment(string articleId, CommentDto commentDto);

        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "Article/Get",
            ResponseFormat = WebMessageFormat.Json)]
        IList<ArticleHeaderDto> GetArticleHeaders();

        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "Article/Get/{articleId}",
            ResponseFormat = WebMessageFormat.Json)]
        ArticleDto GetArticle(string articleId);
    }
}