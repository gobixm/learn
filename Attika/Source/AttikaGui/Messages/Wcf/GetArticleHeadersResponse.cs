using System.Collections.Generic;
using Infotecs.Attika.AttikaGui.DataTransferObjects;

namespace Infotecs.Attika.AttikaGui.Messages.Wcf
{
    public sealed class GetArticleHeadersResponse
    {
        public List<ArticleHeaderDto> Headers { get; set; }
    }
}