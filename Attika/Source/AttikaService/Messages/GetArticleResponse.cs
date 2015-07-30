using Infotecs.Attika.AttikaService.DataTransferObjects;

namespace Infotecs.Attika.AttikaService.Messages
{
    public class GetArticleResponse : BaseMessage
    {
        public ArticleDto Article { get; set; }
    }
}