using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;

namespace Infotecs.Attika.AttikaService
{
    [ServiceContract]
    public interface IArticleApiService
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "api/get",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        void Get(Message message);

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "api/post",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        void Post(Message message);

        [OperationContract]
        [WebInvoke(Method = "DELETE",
            UriTemplate = "api/delete",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        void Delete(Message message);
    }
}