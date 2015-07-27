using GalaSoft.MvvmLight.Messaging;
using Infotecs.Attika.AttikaGui.DTO;

namespace Infotecs.Attika.AttikaGui.GuiMessages
{
    public class AddArticleMessage : MessageBase
    {
        public ArticleDto Article { get; set; }
    }
}