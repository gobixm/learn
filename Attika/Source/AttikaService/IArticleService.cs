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
            UriTemplate = "Article/{articleId}/Comment/New",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        void AddComment(string articleId, CommentDto commentDto);

        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "Article",
            ResponseFormat = WebMessageFormat.Json)]
        IList<ArticleHeaderDto> GetArticleHeaders();

        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "Article/{articleId}",
            ResponseFormat = WebMessageFormat.Json)]
        ArticleDto GetArticle(string articleId);

        [OperationContract]
        [WebInvoke(Method = "DELETE",
            UriTemplate = "Article/{articleId}/Delete")]
        void DeleteArticle(string articleId);

        [OperationContract]
        [WebInvoke(Method = "DELETE",
            UriTemplate = "Comment/{commentId}/Delete")]
        void DeleteComment(string commentId);
    }
}