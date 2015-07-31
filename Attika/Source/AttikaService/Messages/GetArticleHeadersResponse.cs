using System.Collections.Generic;
using Infotecs.Attika.AttikaService.DataTransferObjects;

namespace Infotecs.Attika.AttikaService.Messages
{
    public sealed class GetArticleHeadersResponse : BaseMessage
    {
        public List<ArticleHeaderDto> Headers { get; set; }
    }
}